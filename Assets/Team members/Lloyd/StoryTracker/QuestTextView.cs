using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

public class QuestTextView : MonoBehaviour
{
    
    
    [Button]
    public void QuestStarted()
    {
        //questTracker.SetText(questName+": STARTED!", new Color(1, 1, 1, 1));
    }

    [Button]
    public void QuestCompleted()
    {
       // questTracker.SetText(questName+": COMPLETED!", new Color(.3f, 1, .3f, .5f));
    }

    [Button]
    public void QuestFailed()
    {
       // questTracker.SetText(questName+": FAILED!", new Color(1, 0, 0, .7f));

    }

    [Button]
    public void QuestRestarted()
    {
       // questTracker.SetText(questName+": RESTARTED!", new Color(1, 1, 1, 1));
       // questTracker.MoveQuest(3,questObj,1);
    }
}
