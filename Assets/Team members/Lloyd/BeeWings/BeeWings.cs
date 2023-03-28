using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR;
using Vector3 = UnityEngine.Vector3;

public class BeeWings : MonoBehaviour
{
    [Button]
    public void MeshChangeState(BeeState state)
    {
        ChangeBeeState(state);
    }

    public enum BeeState
    {
        Dead,
        Slow,
        Medium,
        Fast
    }

    public BeeState myState;

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

    private bool flapping = false;
    private bool rotatingToSecond;

    public Vector3 pivotOffset;

    public bool returning;

    public GameObject pivotPoint;


    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();

        originalRotation = transform.localRotation;
        targetRotation = originalRotation * Quaternion.AngleAxis(flapAngle, flapAxis);


        ChangeBeeState(BeeState.Slow);
        flapping = true;

        //StartCoroutine(RotateRigidbodyCoroutine());

        StartCoroutine(FlapWings());
    }

    private float flipThreshold=5;
    float proportionalGain = 100f;

    IEnumerator FlapWings()
    {
        float timeSinceFlap = 0f;
        float expectedAngleDiff = 0f;
        float prevAngleDiff = 0f;

        while (flapping)
        {
            timeSinceFlap += Time.deltaTime;

            expectedAngleDiff = Mathf.Lerp(0f, flapAngle, Mathf.Clamp01(timeSinceFlap / flapTime));

            float angleDiff = Quaternion.Angle(transform.rotation, targetRotation);

            float proportionalTorque = Mathf.Clamp((angleDiff - expectedAngleDiff) * proportionalGain, 0f, flapSpeed);
    
            Vector3 torqueDir = Vector3.Cross(transform.up, targetRotation * Vector3.up);
            rb.AddTorque(torqueDir * proportionalTorque, ForceMode.Force);

            if (angleDiff < flipThreshold)
            {
                targetRotation = Quaternion.AngleAxis(-flapAngle, flapAxis) * transform.rotation;
                timeSinceFlap = 0f;
            }

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        rb.position = pivotPoint.transform.position;

        transform.localRotation *= pivotPoint.transform.localRotation;
    }


    #region BeeState

    public void ChangeBeeState(BeeState newState)
    {
        if (newState == myState)
            return;

        if (newState == BeeState.Dead)
        {
            flapAngle = slowAngle;
            flapSpeed = slowSpeed;

            dying = true;
            StartCoroutine(ReduceFlapSpeed());
            return;
        }

        dying = false;

        if (newState == BeeState.Slow)
        {
            flapAngle = slowAngle;
            flapSpeed = slowSpeed;
            flapTime = slowTime;
        }

        else if (newState == BeeState.Medium)
        {
            flapAngle = medAngle;
            flapSpeed = medSpeed;
            flapTime = medTime;
        }

        else if (newState == BeeState.Fast)
        {
            flapAngle = fastAngle;
            flapSpeed = fastSpeed;
            flapTime = fastTime;
        }

        myState = newState;
        flapping = true;
    }

    #endregion

    private IEnumerator RotateRigidbodyCoroutine()
    {
        while (flapping)
        {
            Quaternion currentRotation = transform.localRotation;

            Quaternion targetRotation = GetTargetRotation();

            Vector3 localFlapAxis = transform.TransformDirection(flapAxis);
            Vector3 parentFlapAxis = transform.parent.TransformDirection(localFlapAxis);

            Vector3 parentUp = transform.parent.TransformDirection(Vector3.up);
            Vector3 rotationAxis = Vector3.Cross(parentFlapAxis, parentUp);

            Vector3 torque = rotationAxis.normalized * flapSpeed * rb.mass;
            rb.AddTorque(torque);

            if (Quaternion.Angle(currentRotation, targetRotation) < 0.1f)
            {
                flapAxis = -flapAxis;
                targetRotation = GetTargetRotation();
            }

            currentRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime / flapTime);

            transform.localRotation = currentRotation;

            transform.position = transform.parent.TransformPoint(Vector3.zero);

            yield return null;
        }
    }

    private Quaternion GetTargetRotation()
    {
        Quaternion parentRotation = transform.parent.rotation;
        Vector3 parentFlapAxis = parentRotation * flapAxis;

        switch (myState)
        {
            case BeeState.Dead:
                return parentRotation * Quaternion.identity;
            case BeeState.Slow:
                return parentRotation * originalRotation * Quaternion.AngleAxis(slowAngle, parentFlapAxis);
            case BeeState.Medium:
                return parentRotation * originalRotation * Quaternion.AngleAxis(medAngle, parentFlapAxis);
            case BeeState.Fast:
                return parentRotation * originalRotation * Quaternion.AngleAxis(fastAngle, parentFlapAxis);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator ReduceFlapSpeed()
    {
        while (dying)
        {
            flapSpeed--;
            yield return new WaitForSeconds(1f);
        }
    }
}