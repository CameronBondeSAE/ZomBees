using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public GameObject neutralMouth;
    public GameObject happy;
    public GameObject sad;
    public GameObject surprised;

    private GameObject currentMouth;
    
    public Dictionary<(CivEmotions, CivEmotions), GameObject> emotionMouthObjects =
        new Dictionary<(CivEmotions, CivEmotions), GameObject>();

    public CivEmotions firstEmotion;
    public CivEmotions secondEmotion;

    public void StartGame()
    {
        GameObject newNeutral = Instantiate(neutralMouth);
        GameObject newHappy = Instantiate(happy);
        GameObject newSad = Instantiate(sad);
        GameObject newSurprised = Instantiate(surprised);
        
        newHappy.SetActive(false);
        newSad.SetActive(false);
        newSurprised.SetActive(false);
        
        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Neutral)] = newNeutral;
        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Happy)] = newHappy;
        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Sad)] = newSad;
        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Surprised)] = newSurprised;

        currentMouth = newNeutral;
    }

    public void ChangeMouth(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        firstEmotion = firstEmote;
        secondEmotion = secondEmote;

        GameObject newMouth = emotionMouthObjects[(firstEmotion, secondEmotion)];

        if (newMouth != currentMouth)
        {
            if (currentMouth != null)
            {
                currentMouth.SetActive(false);
            }

            if (newMouth != null)
            {
                newMouth.SetActive(true);
            }

            currentMouth = newMouth;
        }
    }
}
