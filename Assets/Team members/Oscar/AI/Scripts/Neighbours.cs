using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Neighbours : MonoBehaviour
    {
        public LayerMask comrades;

        private List<GameObject> friendsList = new List<GameObject>();

        //if the little guys are in the same layer as this little guy
        //and
        //enters the collider sphere
        //add it to a list or friends 
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == comrades)
            {
                if (!friendsList.Contains(other.gameObject))
                {
                    friendsList.Add(other.gameObject);
                    print("added " + other.gameObject.name);
                }
            }
        }
        //if it leaves the sphere remove it from the list
        private void OnTriggerExit(Collider other)
        {
            if (friendsList.Contains(other.gameObject))
            {
                friendsList.Remove(other.gameObject);
                print("removed " + other.gameObject.name);
            }
        }
    }
}

