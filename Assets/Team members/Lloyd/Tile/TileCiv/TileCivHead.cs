using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCivHead : MonoBehaviour
{
    public Vector3 target;
    public float torqueSpeed;
    public Rigidbody rb;

    private Quaternion targetRotation;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
    }

    private void FixedUpdate()
    {
        if (target == null)
            transform.LookAt(transform.position + transform.forward, transform.up);
        
        Vector3 targetDir = target - transform.position;
        targetRotation = Quaternion.LookRotation(targetDir, transform.up);
            Quaternion rotation =
                Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * torqueSpeed);
            rb.MoveRotation(rotation);
    }
}
