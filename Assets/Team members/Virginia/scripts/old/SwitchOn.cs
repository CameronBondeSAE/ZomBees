using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOn : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("light is on");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("light is still on");
    }

    void OnDisable()
    {
        Debug.Log("light is off");
    }
}
