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
            vision = GetComponent<OscarVision>();
        }

        public void Update()
        {
            if (hearing.heardSound)
            {
                foreach (SoundProperties sounds in hearing.soundsList)
                {
                    if (sounds.SoundType == SoundEmitter.SoundType.CreatureRepellant)
                        heardUnpleasantNoise = true;

                    else
                    {
                        heardNoise = true;
                    }
                }
            }

            else
            {
                heardNoise = false;
                heardUnpleasantNoise = false;
            }
                
                
            if (vision.civsInSight.Count > 0)
            {
                seesCiv = true;
            }
        }

        #region AntAI

        public bool heardNoise;

        public bool bitCiv;

        public bool seesCiv;

        public bool heardUnpleasantNoise;

        public bool seesLight;

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            {
                aWorldState.Set(HalfZombeeScenario.HeardNoise, heardNoise);

                aWorldState.Set(HalfZombeeScenario.BitCiv, bitCiv);

                aWorldState.Set(HalfZombeeScenario.SeesCiv, seesCiv);

                aWorldState.Set(HalfZombeeScenario.HeardUnpleasantNoise, heardUnpleasantNoise);

                aWorldState.Set(HalfZombeeScenario.SeesLight, seesLight);
            }
        }

        #endregion

        #region Hearing

        public Hearing hearing;

        #endregion

        #region Vision

        public OscarVision vision;

        #endregion
    }
}