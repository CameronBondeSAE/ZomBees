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
        public Feeler feel;
        private float distance = 5f;
        private int direction = 1;
        private float spinTimer;

    
        public void FixedUpdate()
        {
            RaycastHit hitInfo = feel.GetHitInfo();
            if (hitInfo.collider != null)
            {
                guy.rb.AddRelativeTorque(Vector3.up, ForceMode.VelocityChange);
            }
        }
        
    }
}

