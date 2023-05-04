using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

namespace Marcus
{
    public class TetheredObject : DynamicObject
    {
        private Vector3 targetPosition;
        
        public bool tetheredObject;
        public Transform target;
        public float tetherStrength;

        private void FixedUpdate()
        {
            if (target && tetheredObject)
            {
                TurnTowards(GetComponent<Rigidbody>(), target, tetherStrength);
            }
        }

        public void TurnTowards(Rigidbody me, Transform targetTransform, float turnSpeed)
        {
            targetPosition = targetTransform.position;
            
            Vector3 targetDirection = targetPosition - me.transform.position;
            if (me.transform.forward != targetDirection)
            {
                me.AddTorque(Vector3.Cross(me.transform.forward, targetDirection).normalized * turnSpeed);
            }
        }
    }
}
