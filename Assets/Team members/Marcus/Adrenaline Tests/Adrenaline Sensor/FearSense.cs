using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FearSense : MonoBehaviour, IAdrenalineSensitive
    {
        private bool hasObjective;
        
        private void Update()
        {
            
        }
        
        public void PathfindToSource(Vector3 searchPos)
        {
            if (!hasObjective)
            {
                print(searchPos);
                hasObjective = true;
            }
        }
    }
}
