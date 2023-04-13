using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerSimple : MonoBehaviour
{
    private float time;

    //quests to begin
    public List<CharacterQuestEvent> eventsList;

    public float checkInterval = 1.0f;

    private WorldTime worldTime;

    private int currentIndexInt = 0;

    private void Update()
    {
        time = worldTime.time;
    }

    private void Start()
    {
        StartCoroutine(CheckTime());
        worldTime = GetComponent<WorldTime>();
    }

    private IEnumerator CheckTime()
    {
        while (currentIndexInt < eventsList.Count)
        {
            float currentTime = worldTime.time;

            if (currentTime >= eventsList[currentIndexInt].time)
            {
                //logic goes here
                Debug.Log(eventsList[currentIndexInt].descriptionForGPT);
                currentIndexInt++;
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}