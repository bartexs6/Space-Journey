using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

[System.Serializable]
public class QuestGiver : MonoBehaviour
{
    private static JsonData jsonData;
    private static Dictionary<string, string[]> namesDictionary = new Dictionary<string, string[]>();

    private Quest questToGive;
    private int id = 0;

    private List<int> takenQuests = new List<int>();
    private List<int> questsToGive = new List<int>();
    
    private enum NPCs
    {
        Terminal,
        Srerminal
    }
    [SerializeField]
    new NPCs name;

    private void Start()
    {
        LoadQuests();
    }

    public void GiveQuest()
    {  
        if(!takenQuests.Contains(id))
        {
            Debug.Log("Nowe Zadanie");
            takenQuests.Add(id);
            questToGive = Quest.CreateQuest(id);
            UI.createNotification($"Nowe zadanie: {questToGive.title}", 4);
        }      
    }

    void LoadQuests()
    {
        string[] questNames = namesDictionary[name.ToString()];
        TextAsset load = Resources.Load<TextAsset>("GameData/questData");
        jsonData = JsonMapper.ToObject(load.text);

        foreach (string item in questNames)
        {
            for (int i = 0; i < jsonData.Count; i++)
            {
                if(item == jsonData[i]["title"].ToString())
                {
                    questsToGive.Add((int)jsonData[i]["id"]);
                    break;
                }
            }
        }

        foreach (int id in questsToGive)
        {
            //Debug.Log(id);
        }
        
    }

    public static void LoadNpcData()
    {
        TextAsset load = Resources.Load<TextAsset>("GameData/npcList");
        jsonData = JsonMapper.ToObject(load.text);

        for (int i = 0; i < jsonData.Count; i++)
        {
            int length = jsonData[i]["quests"].Count;
            string[] questArray = new string[length];
            for (int j = 0; j < length; j++)
            {
                questArray[j] = jsonData[i]["quests"][j].ToString(); 
            }
            namesDictionary.Add(jsonData[i]["name"].ToString(), questArray);
        }

    }

}



