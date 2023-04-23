using UnityEngine;

namespace Johns
{
    public class StateManager : MonoBehaviour
    {
        public StateBase startingState;
        public StateBase currentState;
 
        // Set a default state
        private void Start()
        {
            ChangeState(startingState);
        }

        // This works for ANY STATE
        public void ChangeState(StateBase newState)
        {
            // Check if the state is the same and DON'T swap
            if (newState == null || newState == currentState)
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


