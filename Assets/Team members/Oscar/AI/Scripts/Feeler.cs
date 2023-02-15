using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Feeler : MonoBehaviour
    {
        public LittleGuy guy;
        private RaycastHit hitInfo;
        private float distance = 5f;
        public float feelerAmount;
        
        private void FixedUpdate()
        {
            for (int i = 0; i < feelerAmount; i++)
            {
                Vector3 direction = Quaternion.Euler(0f, i + 2, 0f) * guy.transform.forward;
                if (Physics.Raycast(guy.rb.transform.localPosition, direction, out hitInfo, distance, 255, QueryTriggerInteraction.Ignore))
                {
                    
                }
            }
            
        }
    }
}

