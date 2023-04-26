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
        public LittleGuy littleGuy;
        public OscarVision vision;
        public CivGPT civGPT;
        public CivilianModel civMod;
        public CivilianTraits civTraits;
        public TraitScriptableObject fear;
        public TraitScriptableObject hunger;
        
        public Inventory inventory;
        public Hearing ears;
        
        public bool iAmScared;
        public bool iAmFollowing;
        public bool iDeliveredStuff;
        public bool iAmIdle;
        public bool iAmAlive;
        public bool iAmHiding;
        public bool iAmConversing;

        private void OnEnable()
        {
            civGPT.GPTPerformingActionEvent += GPTPerformingAction;
        }
        
        private void GPTPerformingAction(object sender, CivGPT.CivAction actionFromCiv)
        {
            if (actionFromCiv == CivGPT.CivAction.RunAway)
            {
                civTraits.GetTrait(fear).value = civTraits.GetTrait(fear).threshold;
            }
        }

        public bool AmIIdle()
        {
            return iAmIdle;
        }
        public bool AmIAlive()
        {
            return iAmAlive;
        }
        public bool AmIFollowing()
        {
            if (vision.civsInSight.Count >= 1 && civTraits.GetTrait(fear).thresholdHit)
            {
                iAmFollowing = true;
            }

            if (iAmFollowing == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeliverTheRocks()
        {
            return false;
        }
        public bool AmIConversing()
        {
            return iAmConversing;
        }
        public bool AmIScared()
        {
            return civTraits.GetTrait(fear).thresholdHit;
        }
        public bool ShouldIHide()
        {
            return iAmHiding;
        }
        public bool CanISeeRocks()
        {
            return vision.objectsInSight.Count > 0;
        }
        public bool DoIHaveRocks()
        {
            if (inventory.heldItem != null)
            {
                if (inventory.heldItem.Description() == "Rock")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool GetTheStuff()
        {
            return false;
        }

        public bool DoIHaveStuff()
        {
            if (inventory.heldItem != null)
            {
                if (inventory.heldItem.Description() == "Collected Item")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool StuffDelivered()
        {
            return false;
        }

        public bool ImHungry()
        {
            return civTraits.GetTrait(hunger).thresholdHit;
        }

        public bool DoIHaveFood()
        {
            if (inventory.heldItem != null)
            {
                if (inventory.heldItem.Description() == "Food")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool ISeeFood()
        {
            return vision.foodInSight.Count > 0;
        }
    }
}