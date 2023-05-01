using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lloyd
{
    public class Eyebrows : MonoBehaviour
    {
        //hacked together with GPT
        //Eyebrows has esoteric knowledge of local transform positions and rotations
        //moves leftEyebrow+rightEyebrow according to the invoked CivEmotion

        public Transform leftEyebrow;
        public Transform rightEyebrow;

        public Vector3 neutralPos;
        public Vector3 surprisedPos;

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
        
        [ShowInInspector] public Dictionary<(CivEmotions, CivEmotions), Vector3> emotionVector3s =
            new Dictionary<(CivEmotions, CivEmotions), Vector3>();

        [ReadOnly]
        public float waitTime;

        [Button]
        public void StartGame(float newWaitTime)
        {
            emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Neutral)] = neutralRotate;
            emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Angry)] = angry;
            emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Happy)] = happy;
            emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Sad)] = sad;
            emotionQuaternions[(CivEmotions.Neutral, CivEmotions.Surprised)] = surprised;

            emotionVector3s[(CivEmotions.Neutral, CivEmotions.Neutral)] = neutralPos;
            emotionVector3s[(CivEmotions.Neutral, CivEmotions.Surprised)] = surprisedPos;

            waitTime = newWaitTime;
        }

        public void ChangeEmotion(CivEmotions newFirstEmote, CivEmotions newSecondEmote)
        {
            firstEmotion = newFirstEmote;
            secondEmotion = newSecondEmote;

            Quaternion targetQuaternion = emotionQuaternions[(firstEmotion, secondEmotion)];

            StartCoroutine(TurnOff());

            Vector3 leftNeutralPos = new Vector3(-neutralPos.x, neutralPos.y, neutralPos.z);
            Vector3 leftSurprisedPos = new Vector3(-surprisedPos.x, surprisedPos.y, surprisedPos.z);

            if (secondEmotion == CivEmotions.Surprised)
            {
                StartCoroutine(MoveToTarget(leftEyebrow, leftSurprisedPos));
                StartCoroutine(MoveToTarget(rightEyebrow, surprisedPos));
            }

            else
            {
                StartCoroutine(MoveToTarget(leftEyebrow, leftNeutralPos));
                StartCoroutine(MoveToTarget(rightEyebrow, neutralPos));
            }

            StartCoroutine(RotateLeftToEmotion(leftEyebrow, targetQuaternion));
            StartCoroutine(RotateRightToEmotion(rightEyebrow, targetQuaternion));
        }

        //this should be DOTween movement

        private IEnumerator RotateLeftToEmotion(Transform targetTransform, Quaternion targetRotation)
        {
            /*if (targetRotation == neutralRotate)
            {
                targetRotation = Quaternion.identity;
            }*/
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
        
        private IEnumerator MoveToTarget(Transform targetTransform, Vector3 targetLocalPosition)
        { 
            Vector3 startLocalPosition = targetTransform.localPosition;
        Vector3 targetPosition = targetTransform.TransformPoint(targetLocalPosition);

        float distanceToTarget = Vector3.Distance(startLocalPosition, targetPosition);
        float moveDistance = 0f;
        float normalizedTime = 0f;

            while (moveDistance < distanceToTarget)
        {
            normalizedTime += Time.deltaTime / (distanceToTarget / rotationSpeed);
            Vector3 newPosition = Vector3.Lerp(startLocalPosition, targetPosition, normalizedTime);
            Vector3 newLocalPosition = targetTransform.InverseTransformPoint(newPosition);
            targetTransform.localPosition = newLocalPosition;
            moveDistance = Vector3.Distance(targetTransform.position, startLocalPosition);
            yield return null;
        }

        targetTransform.localPosition = targetLocalPosition;
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
}