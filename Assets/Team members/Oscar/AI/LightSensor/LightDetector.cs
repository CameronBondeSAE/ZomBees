using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // detect the bool
        if (other != null)
        {
            if (other.GetComponent<DynamicObject>())
            {
                if (other.GetComponent<DynamicObject>().isLit == true)
                {
                    print("The Glow Is So Beautiful!");
                }
                else
                {
                    print("I dont see nothing *Shrugs*");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // forgetaboutit!
        
    }
}
