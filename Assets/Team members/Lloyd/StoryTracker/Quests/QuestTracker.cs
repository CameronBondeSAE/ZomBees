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
    
    public static QuestTracker QuestInstance { get; private set; }
    
    #region QuestTracker
    
    public List<QuestScriptable> quests = new List<QuestScriptable>();

    public List<QuestScriptable> questsBegan = new List<QuestScriptable>();

    public List<QuestScriptable> questsCompleted = new List<QuestScriptable>();

    public List<QuestScriptable> questsFailed = new List<QuestScriptable>();

    private List<QuestScriptable>[] questLists;

    public Color textColor;

    private void Awake()
    {
        Initialize();
        if (QuestInstance == null)
        {
            QuestInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        questLists = new List<QuestScriptable>[4];
        for (int i = 0; i < questLists.Length; i++)
        {
            questLists[i] = new List<QuestScriptable>();
        }

        questLists[0] = quests;
        questLists[1] = questsBegan;
        questLists[2] = questsCompleted;
        questLists[3] = questsFailed;
        
        OnFinishedInitializeEvent();
    }
    
    public event Action FinishedInitializeEvent;

    public void OnFinishedInitializeEvent()
    {
        FinishedInitializeEvent?.Invoke();
    }

    public void AddQuest(QuestScriptable quest)
    {
        quests.Add(quest);
    }
    
    public void MoveQuest(int oldListIndex, QuestScriptable questObj, int newListIndex)
    {
        if (questLists[oldListIndex].Contains(questObj))
        {
            questLists[oldListIndex].Remove(questObj);

            questLists[newListIndex].Add(questObj);
        }

        if (newListIndex >= 2)
        {
            
        }

        else
        {
            Debug.LogWarning("Quest not found in 'quests' list.");
        }
    }
    #endregion
    
    #region QuestTextViewShit
    public TMP_Text questText;
    public float fadeSpeed;

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