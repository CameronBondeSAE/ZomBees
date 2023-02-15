using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Oscar
{
    public class Avoid : MonoBehaviour
    {
        public LittleGuy guy;
        private RaycastHit hitInfo;
        private float distance = 5f;
        private int direction = 1;
        private float spinTimer;

        private void Start()
        {
            direction = Random.Range(0,2);
        }
    
        public void FixedUpdate()
        {
            if (Physics.Raycast(guy.rb.transform.localPosition, transform.forward, out hitInfo, distance, 255, QueryTriggerInteraction.Ignore))
            {
                if (direction == 1)
                {
                    guy.rb.AddRelativeTorque(Vector3.up, ForceMode.VelocityChange);
                }
                else
                {
                    guy.rb.AddRelativeTorque(Vector3.down, ForceMode.VelocityChange);
                }
            }
        }
        
    }
}

