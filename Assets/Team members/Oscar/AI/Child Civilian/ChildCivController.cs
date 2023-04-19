using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class ChildCivController : MonoBehaviour
    {        
        public LittleGuy littleGuy;
        public OscarVision vision;
        public OscarCivProfile civProfile;
        
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
            if (ears.heardSound || vision.beesInSight.Count >= 1)
            {
                iAmScared = true;
            }

            return iAmScared;
        }

        public bool CanISeeRocks()
        {
            return vision.objectsInSight.Count >= 1;
        }
        public bool DoIHaveRocks()
        {
            return littleGuy.collectedObjects.Count >= 3;
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
            return false;
        }

        public bool ISeeFood()
        {
            return false;
        }

        public bool ShouldIHide()
        {
            return iAmHiding;
        }
    }
}