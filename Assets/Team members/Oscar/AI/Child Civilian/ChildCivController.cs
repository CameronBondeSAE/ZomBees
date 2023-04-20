using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Oscar
{
    public class ChildCivController : MonoBehaviour
    {        
        public LittleGuy littleGuy;
        public OscarVision vision;
        public OscarCivProfile civProfile;

        public Inventory inventory;
        public Hearing ears;
        
        public bool iAmScared;
        public bool iAmFollowing;
        public bool iDeliveredStuff;
        public bool iAmIdle;
        public bool iAmAlive;
        public bool iAmHiding;
        
        //public bool playerTalking;

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
            if (vision.civsInSight.Count >= 1 && iAmScared == true)
            {
                iAmFollowing = true;
            }

            if (iAmScared == true && iAmFollowing == true)
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
            return false;
        }
        public bool AmIScared()
        {
            if (ears.heardSound || vision.beesInSight.Count > 0 || civProfile.fearLevel >= .6f)
            {
                iAmScared = true;
            }

            return iAmScared;
        }

        public bool CanISeeRocks()
        {
            return vision.objectsInSight.Count > 0;
        }
        public bool DoIHaveRocks()
        {
            // if (inventory.hand != null)
            // {
            //     if (inventory.hand.GetComponent<DynamicObject>().isObject)
            //     {
            //         return true;
            //     }
            //     else
            //     {
            //         return false;
            //     }
            // }
            return false;
        }

        public bool GetTheStuff()
        {
            return false;
        }

        public bool DoIHaveStuff()
        {
            return littleGuy.collectedObjects.Count >= 3;
        }

        public bool StuffDelivered()
        {
            return false;
        }

        public bool ImHungry()
        {
            return civProfile.hungerLevel >= 0.6f;
        }

        public bool DoIHaveFood()
        {
            if (inventory.hand != null)
            {
                if (inventory.hand.GetComponent<DynamicObject>().isFood)
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

        public bool ShouldIHide()
        {
            return iAmHiding;
        }
    }
}