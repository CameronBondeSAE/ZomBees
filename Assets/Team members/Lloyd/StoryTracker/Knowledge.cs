using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
    public QuestScriptable quest;

    public CivilianEmotions emotes;
    
    private QuestTracker tracker;
    private void OnEnable()
    {
        quest.QuestEvent += StartQuest;
    }

    private void StartQuest(string questName, QuestScriptable.QuestStatus status)
    {
        if(status == QuestScriptable.QuestStatus.Began)
            emotes.ModifyDictionary("fear", 10f);
    }

    private void Update()
    {
        quest.Update();
    }

    private void OnDisable()
    {
        quest.QuestEvent -= StartQuest;
    }
}
