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

    private void OnEnable()
    {
        questObj = new GameObject(questName);
        questObj.transform.SetParent(gameObject.transform);

        Quests.Instance.AddQuest(questObj, questName);
    }

    [Button]
    public void QuestStarted()
    {
        Quests.Instance.MoveQuest(0,questObj,1);
        Quests.Instance.SetText(questName+": STARTED!", new Color(1, 1, 1, 1));
    }

    [Button]
    public void QuestCompleted()
    {
        Quests.Instance.MoveQuest(1, questObj, 2);
        Quests.Instance.SetText(questName+": COMPLETED!", new Color(.3f, 1, .3f, .5f));

    }

    [Button]
    public void QuestFailed()
    {
        Quests.Instance.MoveQuest(1, questObj, 3);
        Quests.Instance.SetText(questName+": FAILED!", new Color(1, 0, 0, .7f));

    }

    [Button]
    public void QuestRestarted()
    {
        Quests.Instance.SetText(questName+": RESTARTED!", new Color(1, 1, 1, 1));
        Quests.Instance.MoveQuest(3,questObj,1);
    }
}
