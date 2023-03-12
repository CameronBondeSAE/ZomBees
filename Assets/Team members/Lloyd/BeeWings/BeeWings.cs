using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;
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

    public float medAngle;
    public float medSpeed;

    public float fastAngle;
    public float fastSpeed;

    private bool isRotating = false;

    public Vector3 flapAxis;
    public Rigidbody rb;

    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private bool flapping = false;
    private bool rotatingToSecond;

    public GameObject pivot;
    public Vector3 pivotOffset;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.rotation = pivot.transform.rotation;

        originalRotation = transform.localRotation;
        targetRotation = originalRotation * Quaternion.AngleAxis(flapAngle, flapAxis);


        ChangeBeeState(BeeState.Slow);
        flapping = true;

        pivot.transform.SetParent(transform.parent);
        pivot.transform.localPosition = pivotOffset;
        
        StartCoroutine(RotateRigidbodyCoroutine());

        //StartCoroutine(FlapWings());
    }

    IEnumerator FlapWings()
    {
        Quaternion targetRotation = Quaternion.identity;
        float angleThreshold = 1f;

        while (flapping)
        {
            Vector3 flapAxis = pivot.transform.forward;

            Quaternion currentRotation = rb.rotation;
            Quaternion newRotation = Quaternion.AngleAxis(flapAngle, flapAxis) * currentRotation;
            targetRotation = Quaternion.Slerp(currentRotation, newRotation, flapTime);

            float angleDiff = Quaternion.Angle(rb.rotation, targetRotation);
            while (angleDiff > angleThreshold)
            {
                Vector3 torqueDir = Vector3.Cross(rb.transform.up, targetRotation * Vector3.up);
                rb.AddTorque(torqueDir * flapSpeed, ForceMode.Impulse);

                yield return new WaitForFixedUpdate();

                angleDiff = Quaternion.Angle(rb.rotation, targetRotation);
            }
        }
    }


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
        }

        else if (newState == BeeState.Medium)
        {
            flapAngle = medAngle;
            flapSpeed = medSpeed;
        }

        else if (newState == BeeState.Fast)
        {
            flapAngle = fastAngle;
            flapSpeed = fastSpeed;
        }

        myState = newState;
        flapping = true;
    }

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