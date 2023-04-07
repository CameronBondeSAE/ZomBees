using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public GameObject neutralMouth;
    public GameObject angry;
    public GameObject happy;
    public GameObject sad;
    public GameObject surprised;

    private GameObject currentMouth;

    private float waitTime;

    public Dictionary<(CivEmotions, CivEmotions), GameObject> emotionMouthObjects =
        new Dictionary<(CivEmotions, CivEmotions), GameObject>();

    public CivEmotions firstEmotion;
    public CivEmotions secondEmotion;

    public void StartGame(float newWaitTime)
    {
        GameObject newNeutral = Instantiate(neutralMouth, transform.position, new Quaternion(0f, 0f, 0f, 1f));
        GameObject newAngry = Instantiate(angry, transform.position, new Quaternion(180f, 0f, 0f, 1f));
        GameObject newHappy = Instantiate(happy, transform.position, new Quaternion(0f, 0f, 0f, 1f));
        GameObject newSad = Instantiate(sad, transform.position, new Quaternion(180f, 0f, 0f, 1f));
        GameObject newSurprised = Instantiate(surprised, transform.position, new Quaternion(0f, 0f, 0f, 1f));

        newNeutral.transform.SetParent(transform);
        newAngry.transform.SetParent(transform);
        newHappy.transform.SetParent(transform);
        newSad.transform.SetParent(transform);
        newSurprised.transform.SetParent(transform);

        newNeutral.SetActive(false);
        newHappy.SetActive(false);
        newAngry.SetActive(false);
        newSad.SetActive(false);
        newSurprised.SetActive(false);

        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Neutral)] = newNeutral;
        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Angry)] = newAngry;
        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Happy)] = newHappy;
        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Sad)] = newSad;
        emotionMouthObjects[(CivEmotions.Neutral, CivEmotions.Surprised)] = newSurprised;

        currentMouth = newNeutral;
        waitTime = newWaitTime;
    }

    public void ChangeMouth(CivEmotions firstEmote, CivEmotions secondEmote)
    {
        firstEmotion = firstEmote;
        secondEmotion = secondEmote;

        GameObject newMouth = emotionMouthObjects[(firstEmotion, secondEmotion)];

        currentMouth.SetActive(false);
        newMouth.SetActive(true);

        currentMouth = newMouth;
        StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOff()
    {
        currentMouth.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        currentMouth.SetActive(false);
    }
}