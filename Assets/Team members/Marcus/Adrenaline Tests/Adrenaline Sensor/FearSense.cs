using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FearSense : MonoBehaviour, IAdrenalineSensitive
    {
        private void OnEnable()
        {
            
        }

        private void Update()
        {
            
        }
        
        public void PathfindToSource(object civ, Vector3 searchPos)
        {
            print(searchPos);
        }
    }
}
