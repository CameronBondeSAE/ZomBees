using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class GuyDudeFeelers : MonoBehaviour
    {
        private float feelerLength = 10;

        private float finalForce;

        public float TurnForce()
        {
            finalForce = 0;
            
            RaycastHit feelerInfo;
            Physics.Raycast(transform.position, transform.forward, out feelerInfo, feelerLength, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            if (feelerInfo.collider)
            {
                finalForce = (feelerLength - feelerInfo.distance) * 1.1f;
            }
            
            return finalForce;
        }
    }
}
