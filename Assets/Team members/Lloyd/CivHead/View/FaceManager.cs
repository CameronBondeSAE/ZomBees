using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FaceManager : MonoBehaviour
{
    private Mouth mouth;

    private Eyebrows eyebrows;

    [Button]
    public void StartGame()
    {
        mouth = GetComponentInChildren<Mouth>();
        eyebrows = GetComponent<Eyebrows>();
        
        mouth.StartGame();
        eyebrows.StartGame();

        ChangeEmotionEvent += mouth.ChangeMouth;
        ChangeEmotionEvent += eyebrows.ChangeEmotion;

    }

    public event Action<CivEmotions, CivEmotions> ChangeEmotionEvent;

    [Button]
    public void OnChangeEmotion(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        ChangeEmotionEvent?.Invoke(firstEmote, secondEmote);
    }
}
