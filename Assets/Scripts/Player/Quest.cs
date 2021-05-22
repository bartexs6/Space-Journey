using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
public class Quest

{
#pragma warning disable 414 // TYMCZASOWO

    public bool isActive;
    private int id;
    public string title;
    public string description;
    private int expReward;
    
    public QuestGoal goal;
    private static JsonData questData;
    public static void ReadQuestData()
    {
        TextAsset load = Resources.Load<TextAsset>("GameData/questData");
        questData = JsonMapper.ToObject(load.text);
    }

    public static Quest CreateQuest(int id)
    {
        Quest quest = new Quest();
        quest.id = id;
        quest.goal = QuestGoal.CreateQuestGoal((int)questData[id]["amount"],  (QuestGoal.GoalType)Enum.Parse(typeof(QuestGoal.GoalType), questData[id]["missionType"].ToString()), quest);
        quest.title = questData[id]["title"].ToString();
        quest.description = questData[id]["description"].ToString();
        quest.expReward = (int)questData[id]["experience"];
        quest.isActive = false;
        return quest;
    }

    public void RewardPlayer()
    {
        UI.createNotification($"Brawo, twoj stary ma: {this.expReward} lat", 4);
    }
}
