using System;
using System.Collections;
using Marcus;
using UnityEngine;

namespace Team_members.Lloyd.BeeStinger
{
    public class RotateAway : MonoBehaviour
    {
        public Vector3 target;
        public float torqueMagnitude = 10f;
        public float slowdownRadius = 1f;

        private Rigidbody rb;

        private bool spinning;

        public void StartSpin(Rigidbody rigid, Vector3 newTarget)
        {
            target = newTarget;
            rb = rigid;
            spinning = true;
            StartCoroutine(Spin());
        }

        private IEnumerator Spin()
        {
            while (spinning)
            {
                rb.angularVelocity = Vector3.zero;
                
                Vector3 targetDirection = target - transform.position;
                float angleToTarget = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
                float torqueY = Mathf.Sign(angleToTarget- 180f) * torqueMagnitude;

                float distanceToTarget = Vector3.Distance(transform.position, target);
                float forceMultiplier = Mathf.Clamp01(distanceToTarget / slowdownRadius);

                if (angleToTarget < 0f)
                {
                    torqueY = -torqueY;
                }

                rb.AddTorque(new Vector3(0f, torqueY * forceMultiplier, 0f), ForceMode.Force);

                yield return new WaitForSeconds(1);
            }
        }

        private void OnDisable()
        {
            spinning = false;
        }
    }
}