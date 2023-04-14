using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEngine;

public class QuestTrackerSimple : MonoBehaviour
{
    public float time;

    //quests to begin
    public List<CharacterQuestEvent> eventsList;

    public float checkInterval = 0.7f;

    public WorldTime worldTime;

    private int currentIndexInt = 0;

    private void Start()
    {
        worldTime = WorldTime.Instance;
        StartCoroutine(CheckTime());
    }

    private void Update()
    {
        time = worldTime.time;
    }

    private IEnumerator CheckTime()
    {
        while (currentIndexInt < eventsList.Count)
        {
            
            if (time > eventsList[currentIndexInt].time)
            {
                OnQuestEventStarted(eventsList[currentIndexInt].descriptionForGPT);
                Debug.Log(eventsList[currentIndexInt].descriptionForGPT);
                currentIndexInt++;
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    public event Action<string> QuestEventStarted;

    public void OnQuestEventStarted(string descriptionForGPTString)
    {
        QuestEventStarted?.Invoke(descriptionForGPTString);
    }
}