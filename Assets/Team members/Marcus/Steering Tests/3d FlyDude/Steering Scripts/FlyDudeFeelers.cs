using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FlyDudeFeelers : MonoBehaviour
    {
        private float length;
        private float finalForce;

        void Start()
        {
            length = GetComponentInParent<FlyDudeStats>().feelerLength;
        }
        
        public float TurnForce()
        {
            finalForce = 0;
            
            RaycastHit feelerInfo;
            Physics.Raycast(transform.position, transform.forward, out feelerInfo, length, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            if (feelerInfo.collider)
            {
                finalForce = (length - feelerInfo.distance) * 1.1f;
            }
            
            return finalForce;
        }
    }
}
