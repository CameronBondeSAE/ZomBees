using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;


namespace Virginia
{
   
    public class ProximitySwitchOff : VStateBase
    {
        public Switch Switch;
        private void OnTriggerExit(Collider other)
        {
            Switch.ThingToSwitch?.TurnOff();
            // Debug.Log("off");
            
        }
    }
}
