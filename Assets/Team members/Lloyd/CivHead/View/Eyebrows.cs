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

    private float waitTime;

    [Button]
    public void StartGame(float newWaitTime)
    {
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Neutral)] = neutralRotate;
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Angry)] = angry;
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Happy)] = happy;
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Sad)] = sad;
        emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Surprised)] = surprised;

        waitTime = newWaitTime;
    }

    [Button]
    public void ChangeEmotion(CivEmotions newFirstEmote, CivEmotions newSecondEmote)
    {
        firstEmotion = newFirstEmote;
        secondEmotion = newSecondEmote;

        if (newSecondEmote == CivEmotions.Happy || newSecondEmote == CivEmotions.Surprised)
            return;

        Quaternion targetQuaternion = emotionQuaternions[(firstEmotion, secondEmotion)];

        StartCoroutine(TurnOff());
        StartCoroutine(RotateLeftToEmotion(leftEyebrow, targetQuaternion));
        StartCoroutine(RotateRightToEmotion(rightEyebrow, targetQuaternion));
    }

    private IEnumerator RotateLeftToEmotion(Transform targetTransform, Quaternion targetRotation)
    {
        if (targetRotation == neutralRotate)
        {
            targetRotation = Quaternion.identity;
        }
        Quaternion flipRotation = Quaternion.AngleAxis(180f, Vector3.up);
        Quaternion targetRotationFlipped = flipRotation * targetRotation;
        Quaternion currentRotation = targetTransform.localRotation;
        while (Quaternion.Angle(currentRotation, targetRotationFlipped) > 0.1f)
        {
            currentRotation = Quaternion.RotateTowards(currentRotation, targetRotationFlipped,
                rotationSpeed * Time.deltaTime);
            targetTransform.localRotation = currentRotation;
            yield return null;
        }
    }

    private IEnumerator RotateRightToEmotion(Transform targetTransform, Quaternion targetRotation)
    {
        Quaternion currentRotation = targetTransform.localRotation;
        while (Quaternion.Angle(currentRotation, targetRotation) > 0.1f)
        {
            currentRotation = Quaternion.RotateTowards(currentRotation, targetRotation,
                rotationSpeed * Time.deltaTime);
            targetTransform.localRotation = currentRotation;
            yield return null;
        }
    }

    private IEnumerator TurnOff()
    {
        leftEyebrow.gameObject.SetActive(true);
        rightEyebrow.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        leftEyebrow.gameObject.SetActive(false);
        rightEyebrow.gameObject.SetActive(false);
    }
}