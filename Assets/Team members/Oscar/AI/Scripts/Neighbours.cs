using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Neighbours : MonoBehaviour
    {
        public LayerMask comrades;
        
        public LayerMask enemys;

        public List<Transform> friendsList = new List<Transform>();
        
        public List<Transform> enemyList = new List<Transform>();

        /*
        * This code uses a bitwise operation to check if the layer of the GameObject represented by "other" is included
        * in the LayerMask represented by "comrades". The "&" operator performs a bitwise AND operation between
        * "comrades.value" and a bit mask where the bit for the layer represented by "other" is set to 1. If the result
        * is greater than 0, it means that the bit for the layer represented by "other" is also set to 1 in
        * "comrades.value", indicating that the GameObject is in a layer included in the LayerMask.
        */

        private void OnTriggerEnter(Collider other)
        {
            if ((comrades.value & (1 << other.gameObject.layer)) > 0)
            {
                if (!friendsList.Contains(other.transform))
                {
                    friendsList.Add(other.transform);
                    print("added " + other.gameObject.name);
                }
            }
            if ((enemys.value & (1 << other.gameObject.layer)) > 0)
            {
                if (!enemyList.Contains(other.transform))
                {
                    enemyList.Add(other.transform);
                    print("added " + other.gameObject.name);
                }
            }
        }
    }
}

