using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Oscar
{
    public class Sensor : MonoBehaviour, ISense
    {
        public bool CivilianFound;
        public bool CivilianDead;
        public bool SeeHive;
        public bool NearHive;
        public bool KillReported;
        public bool NearCivilian;
                    
        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.Set("CivilianFound", CivilianFound);
            aWorldState.Set("CivilianDead", CivilianDead);
            aWorldState.Set("SeeHive", SeeHive);
            aWorldState.Set("NearHive", NearHive);
            aWorldState.Set("KillReported", KillReported);
            aWorldState.Set("NearCivilian", NearCivilian);
        }
    }
}

