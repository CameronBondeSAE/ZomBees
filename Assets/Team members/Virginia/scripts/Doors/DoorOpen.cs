using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virginia
{
    public class DoorOpen : VStateBase
    {
        // Start is called before the first frame update

        public GameObject ChildDoor;

        public void OnEnable()
        {


            ChildDoor.SetActive(false);


        }

        public void OnDisable()
        {
            Debug.Log("closed");
        }
    }
}
