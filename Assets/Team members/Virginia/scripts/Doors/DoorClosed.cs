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

            //ChildDoor.SetActive(true);
            ChildDoor.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.5f);
        }

        public void OnDisable()
        {
            Debug.Log("opened");
        }
    }
}