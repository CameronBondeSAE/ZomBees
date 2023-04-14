using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTracker : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;
    public bool sunOn = false;
    public bool moonOn = false;

    public Vector3 sunRotation;
    public Vector3 moonRotation;

    public void Day()
    {
        sunOn = true;
        //sunRotation x time
    }

    public void Night()
    {
        
    }
}
