using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class GuyDudeNeighbours : MonoBehaviour
    {
        public LayerMask friends;
        public List<Transform> neighbourDudes;

        private void OnTriggerEnter(Collider other)
        {
            if ((friends.value & (1 << other.gameObject.layer)) > 0)
            {
                neighbourDudes.Add(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((friends.value & (1 << other.gameObject.layer)) > 0)
            {
                neighbourDudes.Remove(other.transform);
            }
        }
    }
}
