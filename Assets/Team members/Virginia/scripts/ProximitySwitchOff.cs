using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;


namespace Virginia
{
    public class ProximitySwitchOff : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("off");

        }
    }
}
