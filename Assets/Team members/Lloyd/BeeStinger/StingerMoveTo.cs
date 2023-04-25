using System.Collections;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class StingerMoveTo : AntAIState
    {
        public BeeStingerSensor sensor;
    
        public Vector3 moveToTarget;
        public Rigidbody rb;
        public float maxForce = 10f;
        public float minDistance = 1f;
        public float checkInterval = 0.1f;

        public bool minDistReached;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<BeeStingerSensor>();
        }

        private IEnumerator Start()
        {
            while (true)
            {
                float distance = Vector3.Distance(transform.position, moveToTarget);
                if (distance < minDistance)
                {
                    rb.velocity = Vector3.zero;
                    yield break;
                }
                else
                {
                    float force = Mathf.Lerp(0f, maxForce, distance / minDistance);
                    Vector3 direction = (moveToTarget - transform.position).normalized;
                    rb.AddForce(direction * force, ForceMode.Acceleration);
                }
                yield return new WaitForSeconds(checkInterval);
            }
        }
    }
}
