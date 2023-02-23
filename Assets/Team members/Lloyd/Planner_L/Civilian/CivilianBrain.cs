using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using HarmonyLib;
using UnityEngine;

namespace Civilian
{
    public class CivilianBrain : MonoBehaviour, ISense, IHear
    {
        public bool seeBees;

        public bool inCombatDistance;

        private void OnEnable()
        {
           // seeBees = false;
          //  inCombatDistance = false;
        }

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            aWorldState.Set("SeeBees", seeBees);
            aWorldState.Set("InCombatDistance", inCombatDistance);
            aWorldState.EndUpdate();
        }

        public void ReturnPosition(Vector3 myPos)
        {
            myPos = transform.position;
            Debug.Log(myPos);
        }

        public void SoundHeard(GameObject source, int hits)
        {
            Debug.Log("Heard something with " + hits + " number of objects between");
        }
    }
}