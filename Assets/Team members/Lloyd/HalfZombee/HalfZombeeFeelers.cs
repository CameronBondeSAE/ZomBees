using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfZombeeFeelers : MonoBehaviour
{
    public int numRays;
    private float maxAngle = 90f;
    public float initialRaycastDistance = 8f;
    public float raycastDistanceDecay = 0.8f;

    void Update()
    {
        float angleBetweenRays = Mathf.Clamp(maxAngle / (numRays - 1), 0f, maxAngle);

        float currentRaycastDistance = initialRaycastDistance;
        for (int i = 0; i < numRays; i++)
        {
            float angleToFire = i * angleBetweenRays;

            if (i % 2 == 0)
                angleToFire = -angleToFire;

            Vector3 directionToFire = Quaternion.AngleAxis(angleToFire, transform.up) * transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToFire, out hit, currentRaycastDistance))
            {
                Vector3 directionToHit = hit.point - transform.position;
                float angleToHit = Vector3.SignedAngle(-transform.forward, directionToHit, transform.up);

                Rigidbody rigidbody = GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.AddRelativeTorque(0f, angleToHit, 0f);
                }
            }

            if(i % 2 == 0)
            currentRaycastDistance *= raycastDistanceDecay;
        }
    }
}