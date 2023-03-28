using System;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using TMPro;
using Lloyd;

public class QuestTracker : MonoBehaviour
{
    // QuestTracker tracks the current List of quests
    
    // quests can be Began, Completed and Failed
    // quests are move to the appropriate list 

    public bool initialized = false;

    #region QuestTracker

    public List<QuestScriptable> quests = new List<QuestScriptable>();

    public List<QuestScriptable> questsBegan = new List<QuestScriptable>();

    public List<QuestScriptable> questsCompleted = new List<QuestScriptable>();

    public List<QuestScriptable> questsFailed = new List<QuestScriptable>();

    private List<List<QuestScriptable>> questLists = new List<List<QuestScriptable>>();

    private WorldTime time;

    public void StartGame(WorldTime worldTime)
    {
        time = worldTime;
        Initialize();
    }

    private void Initialize()
    {
        questLists.Add(quests);
        questLists.Add(questsBegan);
        questLists.Add(questsCompleted);
        questLists.Add(questsFailed);

        foreach (QuestScriptable scriptable in quests)
        {
            scriptable.Begin(this, time);
        }
            
        initialized = true;
    }
    
    public void MoveQuest(int oldListIndex, QuestScriptable questObj, int newListIndex)
    {
        if (questLists[oldListIndex].Contains(questObj))
        {
            questLists[oldListIndex].Remove(questObj);
            questLists[newListIndex].Add(questObj);
        }
        else
        {
            Debug.LogWarning("Quest not found in 'quests' list.");
        }
    
        //event for completed / failed
        
    }
    #endregion
    
    #region QuestTextViewShit
    public TMP_Text questText;
    public float fadeSpeed;
    
    public Color textColor;

    private bool fading = true;

    public void SetText(string questString, Color color)
    {
        questText.color = color;
        questText.text = questString;
        fading = true;
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        while (fading)
        {
            Color textColor = questText.color;
            float alpha = Mathf.Max(0, textColor.a - (Time.deltaTime * fadeSpeed));
            textColor.a = alpha;
            questText.color = textColor;

            if (alpha <= 0)
                yield return null;

            else fading = false;
        }
    }
    #endregion
}