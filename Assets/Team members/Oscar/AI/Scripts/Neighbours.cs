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

