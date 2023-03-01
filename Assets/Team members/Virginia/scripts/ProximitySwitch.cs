using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public class ProximitySwitch : MonoBehaviour
    {
        // Start is called before the first frame update
       private void TriggerOnEnter(Collider other)
        {
            Debug.Log("on");

        }

    
    }
}
