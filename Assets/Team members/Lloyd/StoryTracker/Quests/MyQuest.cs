using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using Lloyd;

public class MyQuest : MonoBehaviour
{
    public string questName;

    public string questString;
    
    public GameObject questObj;

    public QuestTracker questTracker;

    private void OnEnable()
    {
        questObj = new GameObject(questName);
        
        questObj.transform.SetParent(gameObject.transform);

        questTracker = GetComponent<QuestTracker>();
        
        questTracker.AddQuest(questObj, questName);
    }

    [Button]
    public void QuestStarted()
    {
        questTracker.MoveQuest(0,questObj,1);
        questTracker.SetText(questName+": STARTED!", new Color(1, 1, 1, 1));
    }

    [Button]
    public void QuestCompleted()
    {
        questTracker.MoveQuest(1, questObj, 2);
        questTracker.SetText(questName+": COMPLETED!", new Color(.3f, 1, .3f, .5f));

    }

    [Button]
    public void QuestFailed()
    {
        questTracker.MoveQuest(1, questObj, 3);
        questTracker.SetText(questName+": FAILED!", new Color(1, 0, 0, .7f));

    }

    [Button]
    public void QuestRestarted()
    {
        questTracker.SetText(questName+": RESTARTED!", new Color(1, 1, 1, 1));
        questTracker.MoveQuest(3,questObj,1);
    }
}
