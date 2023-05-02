using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEngine;

public class QuestTrackerSimple : MonoBehaviour
{
    //quests to begin
    public List<CharacterQuestEvent> eventsList;

    public float checkInterval = 0.7f;

    private int currentIndexInt = 0;

    private void Start()
    {
        StartCoroutine(CheckTime());
    }

    private IEnumerator CheckTime()
    { 
        List<CharacterQuestEvent> eventsListToRemoveFromMain = new List<CharacterQuestEvent>(5);

        while (true)
        {
            foreach (CharacterQuestEvent questEvent in eventsList)
            {
                if (WorldTime.Instance.time > questEvent.TimeToPerformAction)
                {
                    OnQuestEventStarted(questEvent);
                    Debug.Log("QuestTracker: New event = " + questEvent.descriptionForGPT + " : "+questEvent.GridCoordinateForAction);
                    eventsListToRemoveFromMain.Add(questEvent);
                }
            }

            foreach (CharacterQuestEvent questEvent in eventsListToRemoveFromMain)
            {
                eventsList.Remove(questEvent);
            }
            eventsListToRemoveFromMain.Clear();
            yield return new WaitForSeconds(checkInterval);
        }
    }

    public event Action<CharacterQuestEvent> QuestEventStarted;

    public void OnQuestEventStarted(CharacterQuestEvent characterQuestEvent)
    {
        QuestEventStarted?.Invoke(characterQuestEvent);
    }
}