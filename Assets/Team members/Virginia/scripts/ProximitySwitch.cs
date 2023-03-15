using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia

namespace Virginia
{
    public class ProximitySwitch : MonoBehaviour
    {
        // Start is called before the first frame update
       private void OnTriggerEnter(Collider other)
        {
            Debug.Log("on");

        }

    
    }
}
