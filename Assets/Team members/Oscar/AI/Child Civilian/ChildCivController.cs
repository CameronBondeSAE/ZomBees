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
        
        public HearingComp ears;
        public bool iAmScared;
        
        //public bool playerTalking;

        public bool AmIIdle()
        {
            return false;
        }
        public bool AmIAlive()
        {
            return false;
        }
        public bool AmIFollowing()
        {
            if (AmIScared() && vision.civsInSight.Count >= 1)
            {
                return true;
            }

            return false;
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
            if (ears.heardSound)
            {
                iAmScared = true;
            }
            
            if (vision.beesInSight.Count >= 1)
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
            return false;
        }

        public bool StuffDelivered()
        {
            return false;
        }
    }
}