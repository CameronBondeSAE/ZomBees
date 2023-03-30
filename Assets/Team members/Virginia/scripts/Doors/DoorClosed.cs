using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virginia
{
    
    public class DoorClosed : VStateBase
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
}