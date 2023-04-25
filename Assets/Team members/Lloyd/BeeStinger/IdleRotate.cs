using UnityEngine;

namespace Lloyd
{
    public class IdleRotate : MonoBehaviour
    {
        public Vector3 targetObject;
        public Rigidbody rb;
        public float distanceThreshold = 5f;
        public float rotationSpeed = 5f;

        public bool looking = false;
    
        public void StartRotate(Vector3 target, Rigidbody newRb)
        {
            rb = newRb;
            targetObject = target;
            looking = true;
        }
    
        void FixedUpdate()
        {
            if (looking)
            {
                Vector3 targetDirection = targetObject - transform.position;

                float distanceToTarget = Vector3.Distance(transform.position, targetObject);

                if (distanceToTarget < distanceThreshold)
                {
                    Quaternion awayRotation = Quaternion.LookRotation(-targetDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, awayRotation, rotationSpeed * Time.deltaTime);
                    rb.rotation = transform.rotation;
                }
                else
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    rb.rotation = transform.rotation;
                }
            }
        }

        private void OnDisable()
        {
            looking = false;
        }
    }
}
