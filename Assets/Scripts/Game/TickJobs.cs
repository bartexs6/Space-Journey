using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TickJobs
{
    public static List<TickJobs> tickJobsList;
    private static GameObject initGameObject;
    private static void InitIfNeeded()
    {
        if(initGameObject == null)
        {
            initGameObject = new GameObject("TickJobs_InitGameObject");
            tickJobsList = new List<TickJobs>();
        }
    }
    public static TickJobs Create(Action action, int seconds, string jobName = null)
    {
        InitIfNeeded();
        GameObject gameObject = new GameObject("TickJobs", typeof(MonoBehaviourHook));
        int finalTick = TickTimeManager.GetTick() + (int)(seconds/TickTimeManager.TICK_MAX); // 10 tickow to jedna sekunda
        TickJobs tickJob = new TickJobs(action, finalTick, jobName, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = tickJob.Update;

        tickJobsList.Add(tickJob);
        return tickJob;
    }

    public static TickJobs Create(Action action, int endTick, bool isEndTick, string jobName = null)
    {
        InitIfNeeded();
        GameObject gameObject = new GameObject("TickJobs", typeof(MonoBehaviourHook));
        int finalTick = endTick;
        TickJobs tickJob = new TickJobs(action, finalTick, jobName, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = tickJob.Update;

        tickJobsList.Add(tickJob);
        return tickJob;
    }

    private static void StopJob(string jobName)
    {
        for (int i = 0; i < tickJobsList.Count; i++)
        {
            if(tickJobsList[i].jobName == jobName)
            {
                tickJobsList[i].destroyAction();
                i--;
            }
        }
    }

    private static void RemoveJob(TickJobs tickJob)
    {
        InitIfNeeded();
        tickJobsList.Remove(tickJob);
    }

    public class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }
    }
    public Action action;
    private bool isDestroyed;
    private GameObject gameObject;
    public int finalTick;
    public string jobName;

    private TickJobs(Action action, int finalTick, string jobName, GameObject gameObject)
    {
        this.action = action;
        this.finalTick = finalTick;
        this.jobName = jobName;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    public void Update()
    {
        if(TickTimeManager.GetTick() >= finalTick)
        {
            action();
            destroyAction();
        }
    }

    private void destroyAction()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveJob(this);
    }


}
