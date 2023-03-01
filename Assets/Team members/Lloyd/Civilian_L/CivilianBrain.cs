using Anthill.AI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Team_members.Lloyd.Civilian_L
{
    public class CivilianBrain : AntAIAgent, ISense
    {
        private Stats stats;
        public enum CivStates
        {
            Idle,
            Talk,
            Move,
            Interact
        }

        public CivStates myState;
    
        public bool idle;

        public bool inRange;

        public bool talking;

        public bool moving;

        public bool interacting;

        public bool looking;

        public bool hasResource;
    
        private HearingComp hearingComp;
    
        public bool hearingSomething;

        public Transform target;

        public GameObject movingTarget;

        public GameObject talkTarget;

        public bool listening;

        private void OnEnable()
        {
            hearingComp = GetComponent<HearingComp>();
            stats = GetComponent<Stats>();
        }

        [Button("IdleState")]
        public void ChangeIdle()
        {
            ChangeState(CivStates.Idle);
        }
        [Button("MoveState")]
        public void ChangeMove()
        {
            ChangeState(CivStates.Move);
        }
        [Button("InteractState")]
        public void ChangeInteract()
        {
            ChangeState(CivStates.Interact);
        }

        public void ChangeState(CivStates newState)
        {
            if (myState == newState)
            {
                Debug.Log("Same state");
                return;
            }

            idle = false;
            talking = false;
            moving = false;
            interacting = false;
        
        
            myState = newState;

            if (myState == CivStates.Idle)
            {
                idle = true;

                target = movingTarget.transform;

                if (hearingSomething)
                {
                    Transform loudestSound = hearingComp.loudestSound.transform;
                    target = loudestSound.gameObject.transform;
                }
            }

            if (myState == CivStates.Talk)
            {
                talking = true;
                //target = talkingTarget
            }

            if (myState == CivStates.Move)
            {
                moving = true;
                target = movingTarget.transform;
                //target = moving target   
            }

            if (myState == CivStates.Interact)
            {
                interacting = true;
                //target = interact target
            }
        }

        public void FixedUpdate()
        {
            if(hearingComp)
                hearingSomething = hearingComp.heardSound;
        }

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);

            aWorldState.Set("Idle", idle);
        
            aWorldState.Set("Talking", talking);
        
            aWorldState.Set("Moving", moving);
        
            aWorldState.Set("Looking", looking);
        
            aWorldState.Set("HasResource", hasResource);

            aWorldState.Set("Interacting", interacting);
        
            aWorldState.Set("InRange", inRange);

            aWorldState.EndUpdate();
        }
    }
}