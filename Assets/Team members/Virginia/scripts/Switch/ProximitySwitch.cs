using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Virginia
{
 
    public class ProximitySwitch : VStateBase
    {
        public Switch Switch;
        // Start is called before the first frame update
       private void OnTriggerEnter(Collider other)
        {
            Switch.ThingToSwitch?.TurnOn();
            // Debug.Log("on");
            

        }

    
    }
}
