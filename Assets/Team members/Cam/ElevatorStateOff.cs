using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CameronBonde
{

    public class ElevatorStateOff : StateBase
    {
        void OnEnable()
        {
            DoThing();

            Debug.Log("Elevator Off");
        }

        void Update()
        {

        }

        void OnDisable()
        {

        }
    }

}