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

        private bool iAmIdle;
        private bool iAmAlive;
        private bool iAmFollowing;
        private bool iAmConversing;
        private bool iAmScared;
        private bool iAmHungry;
        private bool iAmHiding;
        private bool seeRocks;
        private bool hasRocks;
        private bool deliverRocks;
        private bool getStuff;                        
        private bool hasStuff;
        private bool iDeliveredStuff;
        private bool seeFood;
        private bool hasFood;
                
        #endregion

        private void Awake()
        {
            civGPT.GPTPerformingActionEvent += GPTPerformingAction;        
        }

        private void GPTPerformingAction(object sender, CivGPT.CivAction actionFromCiv)
        {
            switch (actionFromCiv)
            {
                case CivGPT.CivAction.Shoot:
                    // pistol.Shoot();
                    break;
                case CivGPT.CivAction.GatherFood:
                    // Check memories for food, else go to resource points
                    break;
                case CivGPT.CivAction.FollowPlayer:
                    break;
                case CivGPT.CivAction.RunAway:
                    AmIScared = true;
                    break;
                case CivGPT.CivAction.FrozenWithFear:
                    break;
                case CivGPT.CivAction.DoNothing:
                    break;
                case CivGPT.CivAction.CommitSuicide:
                    break;
                case CivGPT.CivAction.Shout:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

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
            get { return iAmScared; }
            set { iAmScared = value; }
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
        
        
        private void FixedUpdate()
        {
            //IDLE |
            
            
            //ALIVE |
            
            
            //FOLLOWING |
            if (vision.civsInSight.Count >= 1 && iAmScared == true)
            {
                AmIFollowing = true;
            }
            else
            {
                AmIFollowing = false;
            }
            
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
            
            //HaveRocks |
            if (inventory.heldItem != null)
            {
                if (inventory.heldItem.Description() == "Rock")
                {
                    DoIHaveRocks = true;
                }
                else
                {
                    DoIHaveRocks = false;
                }
            }
            else
            {
                DoIHaveRocks = false;
            }

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
    }
}