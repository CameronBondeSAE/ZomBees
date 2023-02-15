using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class StateManager : MonoBehaviour
    {        
        public Oscar.StateBase startingState;
        public Oscar.StateBase currentState;
        
        void Start()
        {
            ChangeState(startingState);
        }
    
        void Update()
        {
            
        }
        
        public void ChangeState(StateBase newState)
        {
            if (newState == currentState)
            {
                return;
            }

            if (currentState != null)
            {
                currentState.enabled = false;
            }

            newState.enabled = true;

            currentState = newState;
        }
    }
}

