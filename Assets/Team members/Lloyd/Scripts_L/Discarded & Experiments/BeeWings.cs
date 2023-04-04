using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.XR;
using Vector3 = UnityEngine.Vector3;

public class BeeWings : MonoBehaviour
{
    [Button]
    public void MeshChangeState(BeeWingState wingState)
    {
        ChangeBeeState(wingState);
    }

    public enum BeeWingState
    {
        Dead,
        Slow,
        Medium,
        Fast
    }

    [FormerlySerializedAs("myState")] public BeeWingState myWingState;

    public bool dying;

    public float flapAngle;
    public float flapSpeed;
    public float flapTime;

    public float slowAngle;
    public float slowSpeed;
    public float slowTime;

    public float medAngle;
    public float medSpeed;
    public float medTime;

    public float fastAngle;
    public float fastSpeed;
    public float fastTime;

    private bool isRotating = false;

    public Vector3 flapAxis;
    public Rigidbody rb;

    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private Quaternion parentRotation;

    private bool flapping;
    private bool rotatingToSecond;

    public bool returning;

    public GameObject pivotPoint;


    void Start()
    {
        originalRotation = transform.localRotation;
        targetRotation = originalRotation * Quaternion.AngleAxis(flapAngle, flapAxis);
        
        ChangeBeeState(BeeWingState.Slow);
        
        flapping = true;
        StartCoroutine(FlapWings());
    }

    private void FixedUpdate()
    {
        parentRotation = pivotPoint.transform.rotation;
    }

    IEnumerator FlapWings()
    {
        while (true)
        {
            Quaternion flapRotation = parentRotation * originalRotation * Quaternion.AngleAxis(flapAngle, flapAxis);

            float elapsedFlapTime = 0f;
            while (elapsedFlapTime < flapTime / 2f && flapping)
            {
                float flapStep = Time.deltaTime / (flapTime / 2f);
                elapsedFlapTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedFlapTime * flapStep);
                transform.localRotation = parentRotation * Quaternion.Slerp(originalRotation, flapRotation, t);
                yield return null;
            }

            float elapsedReturnTime = 0f;
            while (elapsedReturnTime < flapTime / 2)
            {
                float returnStep = Time.deltaTime / (flapTime / 2f);
                elapsedReturnTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedReturnTime * returnStep);
                transform.localRotation = parentRotation * Quaternion.Slerp(flapRotation, originalRotation, t);
                yield return null;
            }
        }
    }

    #region BeeState

    public void ChangeBeeState(BeeWingState newWingState)
    {
        if (newWingState == myWingState)
            return;

        if (newWingState == BeeWingState.Dead)
        {
            flapAngle = slowAngle;
            flapSpeed = slowSpeed;

            dying = true;
            StartCoroutine(ReduceFlapSpeed());
            return;
        }

        dying = false;

        if (newWingState == BeeWingState.Slow)
        {
            flapAngle = slowAngle;
            flapSpeed = slowSpeed;
            flapTime = slowTime;
        }

        else if (newWingState == BeeWingState.Medium)
        {
            flapAngle = medAngle;
            flapSpeed = medSpeed;
            flapTime = medTime;
        }

        else if (newWingState == BeeWingState.Fast)
        {
            flapAngle = fastAngle;
            flapSpeed = fastSpeed;
            flapTime = fastTime;
        }

        myWingState = newWingState;
        flapping = true;
    }

    #endregion

    private IEnumerator ReduceFlapSpeed()
    {
        while (dying)
        {
            flapSpeed--;
            yield return new WaitForSeconds(1f);
        }
    }
}