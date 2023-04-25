using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{
    public class StingerRandom : MonoBehaviour
    {
    
        //Chat GPT'd it out
    
        public float sphereRadius = 5f; // radius of sphere to pick points from
        public float forceMagnitude = 10f; // magnitude of force to apply to rigidbody

        private Rigidbody rb;
        private Vector3 targetPoint;
        private bool movingTowardsTarget = false;
        private float timeUntilNextTarget = 0f;

        public void StartRandom(Rigidbody newRb)
        {
            rb = newRb;
            StartCoroutine(RandomMovement());
        }

        public IEnumerator RandomMovement()
        {
            while (true)
            {
                if (!movingTowardsTarget)
                {
                    PickNewTargetPoint();
                }

                Vector3 forceDirection = targetPoint - transform.position;
                rb.AddForce(forceDirection.normalized * forceMagnitude, ForceMode.Force);

                if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
                {
                    movingTowardsTarget = false;
                }

                yield return null;
            }
        }

        void PickNewTargetPoint()
        {
            targetPoint = transform.position + Random.insideUnitSphere * sphereRadius;
            Debug.Log(targetPoint);
            movingTowardsTarget = true;
        }

        private void OnDisable()
        {
            movingTowardsTarget = false;
        }
    }
}