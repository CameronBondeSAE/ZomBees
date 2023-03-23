using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosed : MonoBehaviour
{
    public GameObject ChildDoor;
    public void OnEnable()
    {

        ChildDoor.SetActive(true);
    }

    public void OnDisable()
    {
        Debug.Log("opened");
    }
}
