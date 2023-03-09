using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class LightUpObjects : DynamicObject
{
    private void OnTriggerEnter(Collider other)
    {
        // glow objects
        if (other.GetComponent<Oscar.DynamicObject>())
        {
            other.GetComponent<Oscar.DynamicObject>().isLit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //unglow objects
        if (other.GetComponent<Oscar.DynamicObject>())
        {
            other.GetComponent<Oscar.DynamicObject>().isLit = false;
        }
    }
}
