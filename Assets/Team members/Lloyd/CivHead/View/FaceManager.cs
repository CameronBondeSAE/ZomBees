using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FaceManager : MonoBehaviour
{
    private Mouth mouth;

    private Eyebrows eyebrows;

    public bool emoting = false;

    [Header("How long til features fade")] public float waitTime;

    [Button]
    public void StartGame()
    {
        mouth = GetComponentInChildren<Mouth>();
        eyebrows = GetComponent<Eyebrows>();

        mouth.StartGame(waitTime);
        eyebrows.StartGame(waitTime);

        ChangeEmotionEvent += mouth.ChangeMouth;
        ChangeEmotionEvent += eyebrows.ChangeEmotion;

        Neutral(CivEmotions.Angry, CivEmotions.Angry);
    }

    public event Action<CivEmotions, CivEmotions> ChangeEmotionEvent;

    public void OnChangeEmotionEvent(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        ChangeEmotionEvent?.Invoke(firstEmote, secondEmote);
    }

    private IEnumerator Emoting()
    {
        emoting = true;
        yield return new WaitForSeconds(waitTime);
        emoting = false;
    }

    [Button]
    public void OnChangeEmotion(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        if (!emoting)
        {
            StartCoroutine(Emoting());

            ChangeEmotionEvent?.Invoke(firstEmote, secondEmote);
        }
    }

    [Button]
    public void Neutral(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        firstEmote = CivEmotions.Neutral;
        secondEmote = CivEmotions.Neutral;
        OnChangeEmotion(firstEmote, secondEmote);
    }


    [Button]
    public void Happy(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        firstEmote = CivEmotions.Neutral;
        secondEmote = CivEmotions.Happy;
        OnChangeEmotion(firstEmote, secondEmote);
    }

    [Button]
    public void Sad(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        firstEmote = CivEmotions.Neutral;
        secondEmote = CivEmotions.Sad;
        OnChangeEmotion(firstEmote, secondEmote);
    }

    [Button]
    public void Angry(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        firstEmote = CivEmotions.Neutral;
        secondEmote = CivEmotions.Angry;
        OnChangeEmotion(firstEmote, secondEmote);
    }

    [Button]
    public void Surprised(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        firstEmote = CivEmotions.Neutral;
        secondEmote = CivEmotions.Surprised;
        OnChangeEmotion(firstEmote, secondEmote);
    }
}