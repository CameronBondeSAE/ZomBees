using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virginia
{
    public class StateManager : MonoBehaviour
    {
        public VStateBase startingState;
        public VStateBase currentState;

        // Set a default state
        private void Start()
        {
            foreach (VStateBase vStateBase in GetComponents<VStateBase>())
            {
                vStateBase.enabled = false;
            }

            ChangeState(startingState);
        }

        // This works for ANY STATE
        public void ChangeState( VStateBase newState)
        {
            // Check if the state is the same and DON'T swap
            if (newState == currentState)
            {
                return;
            }

            // At first 'currentstate' will ALWAYS be null

            if (currentState != null)
            {
                currentState.enabled = false;
            }

            newState.enabled = true;

            // New state swap over to incoming state
            currentState = newState;
        }
    }


}
