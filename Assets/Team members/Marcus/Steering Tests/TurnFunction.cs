using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class TurnFunction : MonoBehaviour
    {
        private Vector3 targetPosition;

        public void TurnTowards(Rigidbody me, GameObject targetObject, float turnSpeed)
        {
            targetPosition = targetObject.transform.position;
            
            Vector3 targetDirection = targetPosition - me.transform.position;
            if (me.transform.forward != targetDirection)
            {
                me.AddTorque(Vector3.up * turnSpeed);
            }
        }
    }
}
