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
    
    #region QuestTracker
    
    public List<GameObject> quests = new List<GameObject>();

    public List<GameObject> questsBegan = new List<GameObject>();

    public List<GameObject> questsCompleted = new List<GameObject>();

    public List<GameObject> questsFailed = new List<GameObject>();

    private List<GameObject>[] questLists;

    public Color textColor;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        questLists = new List<GameObject>[4];
        for (int i = 0; i < questLists.Length; i++)
        {
            questLists[i] = new List<GameObject>();
        }

        questLists[0] = quests;
        questLists[1] = questsBegan;
        questLists[2] = questsCompleted;
        questLists[3] = questsFailed;
    }

    public void AddQuest(GameObject quest, string questName)
    {
        quests.Add(quest);
    }
    
    public void MoveQuest(int oldListIndex, GameObject questObj, int newListIndex)
    {
        if (questLists[oldListIndex].Contains(questObj))
        {
            questLists[oldListIndex].Remove(questObj);

            questLists[newListIndex].Add(questObj);
        }

        if (newListIndex >= 2)
        {
            questObj.SetActive(false);
        }

        else

        {
            Debug.LogWarning("Quest not found in 'quests' list.");
        }
    }
    #endregion
    
    #region QuestText
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