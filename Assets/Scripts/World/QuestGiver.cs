using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

[System.Serializable]
public class QuestGiver : MonoBehaviour, IInteractible
{
    private static JsonData jsonData;
    private static Dictionary<string, string[]> namesDictionary = new Dictionary<string, string[]>();

    private Quest questToGive;
    private int id = 0;

    private List<int> takenQuests = new List<int>();
    private List<int> QuestListIDs = new List<int>();

    private List<Quest> QuestsToGive = new List<Quest>();
    
    
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

    public void Interact()
    {
        OpenUI();
    }

    private void OpenUI()
    {
        UiTerminal.UpdateGiver(this);
        UiTerminal.SetUpTerminal(QuestsToGive);
    }

    public void RemoveQuest(Quest q)
    {
        QuestsToGive.Remove(q);
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
                    QuestListIDs.Add((int)jsonData[i]["id"]);
                    break;
                }
            }
        }

        foreach (int id in QuestListIDs)
        {
            QuestsToGive.Add(Quest.CreateQuest(id));
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



