using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class TurnTowards : MonoBehaviour
    {
        public LittleGuy guy;
        public Transform target;
        private Vector3 targetPos;
        
        private RaycastHit hitInfo;
        private float distance = 10f;
        private int direction = 1;
        
        void Update()
        {
            if (target)
            {
                targetPos = target.position;
            }

            Vector3 targetDir = targetPos - transform.position;

            float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);

        }
    }
}

