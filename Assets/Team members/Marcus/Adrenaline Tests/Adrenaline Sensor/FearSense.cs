using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FearSense : MonoBehaviour, IAdrenalineSensitive
    {
        public Vector3 targetPos;

        private void Update()
        {
            if (targetPos != Vector3.zero)
            {
                PathfindToSource();
            }
        }

        public void PathfindToSource()
        {
            print(targetPos);
        }
    }
}
