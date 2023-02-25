using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Marcus
{
    public class Neighbours : MonoBehaviour
    {
        public List<Transform> neighbours;
        public LayerMask friends;

        private void OnTriggerEnter(Collider other)
        {
            if ((friends.value & (1 << other.gameObject.layer)) > 0)
            {
                neighbours.Add(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((friends.value & (1 << other.gameObject.layer)) > 0)
            {
                neighbours.Remove(other.transform);
            }
        }
    }
}
    