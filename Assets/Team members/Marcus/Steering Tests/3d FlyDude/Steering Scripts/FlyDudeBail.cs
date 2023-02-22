using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Marcus
{
    public class FlyDudeBail : MonoBehaviour
    {
        private float distance;
        
        private float directionTimer;
        private int bailDir;
        private float bailSpeed = 50f;

        private float bailForce;

        void Start()
        {
            distance = GetComponentInParent<FlyDudeStats>().bailDistance;
        }
        
        // Update is called once per frame
        void Update()
        {
            directionTimer -= Time.deltaTime;
            if (directionTimer <= 0)
            {
                ChangeDir();
            }
        }
        
        void ChangeDir()
        {
            // Generate either -1 or 1
            bailDir = Random.Range(0, 2) * 2 - 1;
            directionTimer = 1f;
        }

        public float CalculateForce()
        {
            bailForce = 0;
            
            RaycastHit bailInfo;
            Physics.Raycast(transform.position, transform.forward, out bailInfo, distance, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            if (bailInfo.collider)
            {
                bailSpeed += Random.Range(-5.5f, 5.5f);
                bailForce = bailSpeed * bailDir;
            }
            
            return bailForce;
        }
    }
}
