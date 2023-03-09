using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // glow objects
        if (other.GetComponent<Oscar.Character>())
        {
            other.GetComponent<Oscar.Character>().isLit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //unglow objects
        if (other.GetComponent<Oscar.Character>())
        {
            other.GetComponent<Oscar.Character>().isLit = false;
        }
    }
}
