using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;
using Lloyd;

namespace Oscar
{
    public class ChildCivController : MonoBehaviour
    {
        
        #region Variables
        
        public LittleGuy littleGuy;
        public OscarVision vision;
        public CivGPT civGPT;
        public CivilianTraits civTraits;
        public TraitScriptableObject fear;
        public TraitScriptableObject hunger;
        
        public Inventory inventory;
        public Hearing ears;

        private bool iAmIdle = false;
        private bool iAmAlive = false;
        private bool iAmFollowing = false;
        private bool iAmConversing = false;
        private bool iAmScared = false;
        private bool iAmHungry = false;
        private bool iAmHiding = false;
        private bool seeRocks = false;
        private bool hasRocks = false;
        private bool deliverRocks = false;
        private bool getStuff = false;
        private bool hasStuff = false;
        private bool iDeliveredStuff = false;
        private bool seeFood = false;
        private bool hasFood = false;
                
        #endregion

        #region GPTStuff
        private void Awake()
        {
            civGPT.GPTPerformingActionEvent += GPTPerformingAction;        
        }
        
        
        private void GPTPerformingAction(object sender, CivGPT.GPTResponseData gptResponseData)
        {
            switch (gptResponseData.CivAction)
            {
                case CivGPT.CivAction.ActivateLightsToAttractCreatures:
                    break;
                
                case CivGPT.CivAction.ActivateSonicWeapon:
                    break;
                
                case CivGPT.CivAction.CloseDoor:
                    break;
                
                case CivGPT.CivAction.CommitSuicide:
                    break;
                
                case CivGPT.CivAction.DoNothing:
                    AmIIdle = true;
                    AmIScared = false;
                    AmIFollowing = false;
                    break;
                
                case CivGPT.CivAction.DropItem:
                    break;
                
                case CivGPT.CivAction.FindAndShootCreature:
                    break;
                
                case CivGPT.CivAction.FindSafeLocation:
                    break;
                
                case CivGPT.CivAction.FollowOtherCharacter:
                    AmIIdle = false;
                    AmIFollowing = true;
                    break;
                
                case CivGPT.CivAction.GatherFood:
                    break;
                
                case CivGPT.CivAction.RetrieveBomb:
                    break;
                
                case CivGPT.CivAction.RunAndHide:
                    AmIIdle = false;
                    AmIScared = true;
                    AmIFollowing = false;
                    break;
                
                case CivGPT.CivAction.ShootOtherCharacter:
                    break;
            }
        }
        #endregion

        #region GettersAndSetters
        public bool AmIIdle
        {
            get { return iAmIdle; }
            set { iAmIdle = value; }
        }
        public bool AmIAlive
        {
            get { return iAmAlive; }
            set { iAmAlive = value; }
        }

        public bool AmIFollowing
        {
            get { return iAmFollowing; }
            set { iAmFollowing = value; }
        }
        public bool AmIConversing
        {
            get { return iAmConversing; }
            set { iAmConversing = value; }
        }
        public bool AmIScared
        {
            get { return iAmScared; }
            set { iAmScared = value; }
        }
        public bool ImHungry
        {
            get { return iAmHungry; }
            set { iAmHungry = value; }
        }
        public bool ShouldIHide
        {
            get { return iAmHiding; }
            set { iAmHiding = value; }
        }
        public bool CanISeeRocks
        {
            get { return seeRocks; }
            set { seeRocks = value; }
        }
        public bool DoIHaveRocks
        {
            get { return hasRocks; }
            set { hasRocks = value; }
        }
        public bool DeliverTheRocks
        {
            get { return deliverRocks; }
            set { deliverRocks = value; }
        }
        public bool GetTheStuff
        {
            get { return getStuff; }
            set { getStuff = value; }
        }
        public bool DoIHaveStuff
        {
            get { return hasStuff; }
            set { hasStuff = value; }
        }
        public bool StuffDelivered
        {
            get { return iDeliveredStuff; }
            set { iDeliveredStuff = value; }
        }
        public bool ISeeFood
        {
            get { return seeFood; }
            set { seeFood = value; }
        }
        public bool DoIHaveFood
        {
            get { return hasFood; }
            set { hasFood = value; }
        }
        #endregion

        #region UpdateFunction
        private void FixedUpdate()
        {
            //IDLE |
            
            
            //ALIVE |
            
            
            //FOLLOWING |
            
            // if (vision.civsInSight.Count >= 1 && iAmScared == true)
            // {
            //     AmIFollowing = true;
            // }
            // else
            // {
            //     AmIFollowing = false;
            // }
            
            //Conversing |
            
            
            //Scared |
            
                        
            //Hungry |
            if (civTraits.GetTrait(hunger).thresholdHit)
            {
                ImHungry = true;
            }
            else
            {
                ImHungry = false;
            }
            
            //Hide |
            
            
            //SeeRocks |
            if (vision.objectsInSight.Count > 0)
            {
                CanISeeRocks = true;
            }
            else
            {
                CanISeeRocks = false;
            }
            
            //HaveRocks
            

            //DeliverRocks |
            

            //GetStuff |
            
            
            //HaveStuff |
            if (inventory.heldItem != null)
            {
                if (inventory.heldItem.Description() == "Collected Item")
                {
                    DoIHaveStuff = true;
                }
                else
                {
                    DoIHaveStuff = false;
                }
            }
            else
            {
                DoIHaveStuff = false;
            }

            //DeliverStuff |
            
            //SeeFood
            if (vision.foodInSight.Count > 0)
            {
                ISeeFood = true;
            }
            else
            {
                ISeeFood = false;
            }
            
            //HaveFood
            if (inventory.heldItem != null)
            {
                if (inventory.heldItem.Description() == "Food")
                {
                    DoIHaveFood = true;
                }
                else
                {
                    DoIHaveFood = false;
                }
            }
            else
            {
                DoIHaveFood = false;
            }
        }
        #endregion

    }
}