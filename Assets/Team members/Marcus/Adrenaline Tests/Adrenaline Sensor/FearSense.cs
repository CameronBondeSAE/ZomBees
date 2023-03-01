using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Marcus
{
    public class FearSense : MonoBehaviour, IAdrenalineSensitive
    {
        [ReadOnly][SerializeField]
        private bool hasObjective;
        private Vector3 target;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, target) >= 1 && hasObjective)
            {
                Vector3 direction = (target - rb.position).normalized;
                rb.velocity = direction * 5f;
            }
            else
            {
                rb.velocity = Vector3.zero;
                hasObjective = false;
            }
        }
        
        public void PathfindToSource(Vector3 searchPos)
        {
            if (!hasObjective)
            {
                print(searchPos);

                target = searchPos;
                hasObjective = true;
            }
        }
    }
}
