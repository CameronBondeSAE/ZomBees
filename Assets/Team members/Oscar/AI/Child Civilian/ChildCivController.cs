using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class ChildCivController : MonoBehaviour
    {
        public OscarVision vision;
        
        public HearingComp ears;
        private bool hearSounds;
        
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
            return false;
        }
        public bool CanISeeRocks()
        {
            return false;
        }
        public bool DoIHaveRocks()
        {
            return false;
        }
    }
}