using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StateManager holds a List of MonoBehaviours QueenStates.
    
// using the public function FlipState which takes a MonoBehaviour incomingState and bool activated
// FlipState enables / disables incomingState and adds / removes incomingState from ActivatedStatesList based on activated

// FlipState is subscribed to QueenEvent ChangeQueenEvent to activate

public class QueenStateManager : MonoBehaviour
{
    public List<MonoBehaviour> ActivatedStatesList = new List<MonoBehaviour>();

    public List<MonoBehaviour> QueenStates = new List<MonoBehaviour>();

    public MonoBehaviour queenAttack;
    
    public MonoBehaviour queenPatrol;

    public MonoBehaviour queenInvestigateSound;

    private QueenEvent queenEvent;

    private void OnEnable()
    {
        if(!QueenStates.Contains(queenAttack))
            QueenStates.Add(queenAttack);
        
        if(!QueenStates.Contains(queenPatrol))
        QueenStates.Add(queenPatrol);

        queenEvent = GetComponent<QueenEvent>();
        
        queenEvent.ChangeQueenState += FlipState;
        
        queenEvent.OnChangeQueenState(queenPatrol, true);
    }

    public void FlipState(MonoBehaviour incomingState, bool activated)
    {
        incomingState.enabled = activated;

        if (ActivatedStatesList.Contains(incomingState) && !activated)
        {
            ActivatedStatesList.Remove(incomingState);
        }
        else if (!ActivatedStatesList.Contains(incomingState) && activated)
        {
            ActivatedStatesList.Add(incomingState);
        }
    }

    private void OnDisable()
    {
        queenEvent.ChangeQueenState -= FlipState;
    }
}