using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal
{
    private GoalType type;
    private Quest parentingQuest;

    private int requiredAmount;
    private int currentAmount;

    private bool isReached;

    public enum GoalType
    {
        Kill,
        Explore,
        Collonize
    }

    private void EnemyDeathCount()
    {
        currentAmount += 1;
        isReached = (currentAmount >= requiredAmount);
        if (isReached)
        {
            GameEvents.current.onEnemyDeathCount -= EnemyDeathCount;
            parentingQuest.RewardPlayer();        
        }
    } 

    private void ChunkCount()
    {
        currentAmount += 1;
        isReached = (currentAmount >= requiredAmount);
        if (isReached)
        {
            parentingQuest.RewardPlayer();           
        }
    }

    public static QuestGoal CreateQuestGoal(int amount, GoalType type, Quest q)
    {
        QuestGoal g = new QuestGoal();
        g.requiredAmount = amount;
        g.isReached = false;
        g.currentAmount = 0;
        g.type = type;
        g.parentingQuest = q;

        return g;
    }

    public static void ActivateQuest(Quest q)
    {
        switch (q.goal.type)
        {
            case GoalType.Kill:
                GameEvents.current.onEnemyDeathCount += q.goal.EnemyDeathCount;
                break;
            case GoalType.Explore:
                GameEvents.current.onChunkExploreCount += q.goal.ChunkCount;
                break;
            case GoalType.Collonize:
                break;
            default:
                break;
        }
    }

    

}
