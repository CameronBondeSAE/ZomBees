using System.Collections.Generic;
using UnityEngine;

namespace Lloyd
{
    public class FollowerPushback : MonoBehaviour, IFollower
    {
        public float force;
        public float sphereRadius;

        private List<GameObject> followersList = new List<GameObject>();
        private Vector3 originalVelocity;

        private void OnEnable()
        {
            sphereRadius = GetComponent<SphereCollider>().radius;
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject follower = collision.gameObject;
            IFollower followerComp = follower.GetComponent<IFollower>();
            if (followerComp != null)
            {
                followersList.Add(follower);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            GameObject follower = collision.gameObject;
            followersList.Remove(follower);
        }

        private void FixedUpdate()
        {
            originalVelocity = GetComponent<Rigidbody>().velocity;

            Vector3 finalForce = Vector3.zero;
            for (int i = 0; i < followersList.Count; i++)
            {
                Vector3 direction = transform.position - followersList[i].transform.position;
                float distance = direction.magnitude;
                if (distance < sphereRadius)
                {
                    distance = sphereRadius + 0.1f;

                    followersList.Remove(followersList[i]);
                    i--;
                    continue;
                }

                finalForce += direction.normalized * (force / Mathf.Pow(distance, 2));
            }

            GetComponent<Rigidbody>().AddForce(finalForce, ForceMode.Impulse);
            GetComponent<Rigidbody>().velocity = originalVelocity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, sphereRadius);
        }
    }
}
