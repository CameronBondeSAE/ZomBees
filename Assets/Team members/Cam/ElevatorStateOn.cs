using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStateOn : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("ElevatorOnState is On");
    }

    void Update()
    {
        Debug.Log("ElevatorOnState is Updating");
    }

    void OnDisable()
    {
        Debug.Log("ElevatorOnState is off");
    }

}
