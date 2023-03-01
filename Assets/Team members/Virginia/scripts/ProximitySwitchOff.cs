using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class ProximitySwitchOff : MonoBehaviour
    {
        private void TriggerOnExit(Collider other)
        {
            Debug.Log("off");

        }
    }
}
