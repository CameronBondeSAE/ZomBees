using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class HalfZombeeSensor : MonoBehaviour, ISense
    {
        public void OnEnable()
        {
            hearing = GetComponent<Hearing>();
            vision = GetComponent<CivVision>();
        }

        public void Update()
        {
            heardNoise = hearing.heardSound;
            seesCiv = vision.seesCivs;
        }

        #region AntAI

        public bool heardNoise;

        public bool bitCiv;

        public bool seesCiv;

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            {
                aWorldState.Set(HalfZombeeScenario.HeardNoise, heardNoise);

                aWorldState.Set(HalfZombeeScenario.BitCiv, bitCiv);

                aWorldState.Set(HalfZombeeScenario.SeesCiv, seesCiv);
            }
        }

        #endregion

        #region Hearing

        public Hearing hearing;

        #endregion

        #region Vision

        public CivVision vision;

        #endregion
    }
}