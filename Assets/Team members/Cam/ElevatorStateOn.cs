using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameronBonde
{

    public class ElevatorStateOn : StateBase
    {
        public void ExampleFunction()
        {
            Debug.Log("ExampleFunction!!!!");
        }
        
        void OnEnable()
        {
            aVariable = 10;

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

}