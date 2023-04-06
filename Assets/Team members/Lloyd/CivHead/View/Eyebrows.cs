using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Eyebrows : MonoBehaviour
{
    public Transform leftEyebrow;
    public Transform rightEyebrow;

    public Quaternion neutralRotate;
    public Quaternion angry;
    public Quaternion happy;
    public Quaternion sad;
    public Quaternion surprised;

    public CivEmotions firstEmotion;
    public CivEmotions secondEmotion;

    public float rotationSpeed;

    [ShowInInspector] public Dictionary<(CivEmotions, CivEmotions), Quaternion> emotionQuaternions =
        new Dictionary<(CivEmotions, CivEmotions), Quaternion>();

    [Button]
    public void StartGame()
    {
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Neutral)] = neutralRotate;
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Angry)] = angry;
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Happy)] = happy;
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Sad)] = sad;
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Surprised)] = surprised;
        
        leftEyebrow.localRotation *= Quaternion.Euler(0f, 180f, 0f);
    }

    [Button]
    public void ChangeEmotion(CivEmotions newFirstEmote, CivEmotions newSecondEmote)
    {
        firstEmotion = newFirstEmote;
        secondEmotion = newSecondEmote;
        Quaternion targetRotation = emotionQuaternions[(firstEmotion, secondEmotion)];
        StartCoroutine(RotateLeftToEmotion(targetRotation));
        StartCoroutine(RotateRightToEmotion(targetRotation));

    }

    private IEnumerator RotateLeftToEmotion(Quaternion targetRotation)
    {
        while (true)
        {
            leftEyebrow.localRotation = targetRotation = 
                Quaternion.RotateTowards(leftEyebrow.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (leftEyebrow.localRotation == targetRotation)
            {
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator RotateRightToEmotion(Quaternion targetRotation)
    {
        while (true)
        {
            rightEyebrow.localRotation = targetRotation = 
                Quaternion.RotateTowards(rightEyebrow.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (rightEyebrow.localRotation == targetRotation)
            {
                yield break;
            }

            yield return null;
        }
    }
}