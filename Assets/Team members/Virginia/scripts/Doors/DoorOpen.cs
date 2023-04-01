using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Virginia
{
    public class DoorOpen : VStateBase
    {
        // Start is called before the first frame update

        public GameObject ChildDoor;

        public void OnEnable()
        {


           // ChildDoor.SetActive(false);
           ChildDoor.transform.DOLocalRotate(new Vector3(0, 100, 0), 1.5f);


        }

        public void OnDisable()
        {
            Debug.Log("closed");
        }
    }
}
