using System;
using System.Collections;
using Anthill.AI;
using UnityEngine;
using Lloyd;
using Sirenix.OdinInspector;

public class LookAtTarget : MonoBehaviour
{

    //CHAT GPT BABY

    public Transform transformTarget;

    public Vector3 vectorTarget;

    Vector3 targetDir;

    public float torqueSpeed;
    public Rigidbody rb;

    public bool lookingAtPoint;
    public bool lookingAtTransform;

    public bool looking = false;

    public float minAngle;
    public float minAngularVelocity;

    public bool torqueApplied;

    private Matrix4x4 inertiaTensorRotation;

    public void Start()
    {
        torqueApplied = false;
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Rigidbody newBody = new Rigidbody(); 
            newBody = gameObject.AddComponent<Rigidbody>();

            newBody.mass = 1f;
            newBody.drag = 0f;
            newBody.angularDrag = 0f;

            newBody.useGravity = false;

            rb = newBody;
        }   
    }

    [Button]
    public void SetVectorTarget(Vector3 vecTarget)
    {
        looking = false;
        vectorTarget = vecTarget;
        lookingAtTransform = false;
        lookingAtPoint = true;
        looking = true;
    }

    [Button]
    public void SetTarget(Transform newTarget)
    {
        looking = false;
        transformTarget = newTarget;
        lookingAtTransform = true;
        lookingAtPoint = false;
        targetRotation = newTarget.rotation;
        looking = true;
    }

    private void FixedUpdate()
    {
        if (looking)
        {
            if (lookingAtTransform)
            {
                targetDir = transformTarget.position - transform.position;
            }
            else if (lookingAtPoint)
            {
                targetDir = vectorTarget - transform.position;
            }
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDir, transform.up);
            
            targetRotation.x = 0;

            // Rotate towards world up vector
            Quaternion upRotation = Quaternion.FromToRotation(transform.up, Vector3.up);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation * upRotation, Time.fixedDeltaTime * torqueSpeed);
            rotation.Normalize();
            
            rb.MoveRotation(rotation);
            rb.transform.rotation = rotation;
        }
    }

    private Quaternion targetRotation;

    /*private void Update()
    {
        if (looking)
        {
            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            angle = Quaternion.Angle(transform.rotation, targetRotation);

            if (angle > minAngle)
            {
                if (!torqueApplied)
                    StartCoroutine(RotateRigidbodyCoroutine(angle));
            }
        }
    }

    private IEnumerator RotateRigidbodyCoroutine(float currAngle)
    {
        torqueApplied = true;

        Matrix4x4 inertiaTensorRotation = Matrix4x4.identity;

        Vector3 torque = Vector3.Cross(transform.forward, targetDir).normalized * currAngle * torqueSpeed;
        float maxAngularVelocity = rb.maxAngularVelocity;
        Vector3 oppositeTorqueVector =
            new Vector3(inertiaTensorRotation.m02, inertiaTensorRotation.m12, inertiaTensorRotation.m22);

        float requiredForceMagnitude = oppositeTorqueVector.magnitude / maxAngularVelocity;

        Vector3 oppositeForce = -oppositeTorqueVector.normalized * requiredForceMagnitude;
        
        Vector3 totalTorque = torque - oppositeForce;
        rb.AddTorque(totalTorque, ForceMode.VelocityChange);

        yield return new WaitForFixedUpdate();

        if (currAngle < minAngle && rb.angularVelocity.magnitude < minAngularVelocity)
        {
            rb.angularVelocity = Vector3.zero;
        }
        
        torqueApplied = false;
        */

        /*
        One thing to note is that you might want to adjust the requiredForceMagnitude calculation to take into account the mass of the rigidbody. Currently, it assumes a mass of 1, so the opposite force might be too weak or too strong depending on the mass of the rigidbody. You could multiply requiredForceMagnitude by rb.mass to adjust for this.
    }}*/
}