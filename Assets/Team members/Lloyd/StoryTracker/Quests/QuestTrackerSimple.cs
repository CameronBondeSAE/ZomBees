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
    
    private void Start()
    {
        worldTime = GetComponent<WorldTime>();
        StartCoroutine(CheckObjectTimes());
    }

    private void Update()
    {
        time = worldTime.time;
    }

    private IEnumerator CheckObjectTimes()
    {
        while (true)
        {
            foreach (CharacterQuestEvent scriptableObject in eventsList)
            {
                if (Math.Abs(scriptableObject.time - time) < .1f)
                {
                    Debug.Log(scriptableObject.descriptionForGPT);
                    eventsList.Remove(scriptableObject);
                }
            }
            yield return new WaitForSeconds(checkInterval);
        }
    }
}