using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class AvoidBee : MonoBehaviour
    {
        public BeeGuy guy;
        private RaycastHit hitInfo;
        private float distance = 5f;
        private int direction = 1;
        private float spinTimer;
        
        private void Start()
        {
            direction = Random.Range(0,2);
            print(direction);
        }
    
        public void FixedUpdate()
        {
            if (Physics.Raycast(guy.rbee.transform.localPosition, transform.forward, out hitInfo, distance, 255, QueryTriggerInteraction.Ignore))
            {
                if (direction == 1)
                {
                    guy.rbee.AddRelativeTorque(Vector3.up, ForceMode.VelocityChange);
                }
                else
                {
                    guy.rbee.AddRelativeTorque(Vector3.down, ForceMode.VelocityChange);
                }
            }
        }

    }
}

