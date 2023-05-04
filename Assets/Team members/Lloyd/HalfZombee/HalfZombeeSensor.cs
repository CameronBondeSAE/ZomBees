using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Utilities;

namespace Lloyd
{
    public class HalfZombeeSensor : DynamicObject, ISense
    {
        public void OnEnable()
        {
            hearing = GetComponent<Hearing>();
            health = GetComponent<Health>();
            health.HealthReducedToZeroEvent += Death;
            beeWings.SetWings();
            infected.StartTicking();

            isBee = true;
            isCiv = true;

            description = "A tragic figure of a broken human, halfway through metamorphosis.";

            importance = .5f;
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

        public bool dead;

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            {
                aWorldState.Set(HalfZombeeScenario.HeardNoise, heardNoise);

                aWorldState.Set(HalfZombeeScenario.BitCiv, bitCiv);

                aWorldState.Set(HalfZombeeScenario.SeesCiv, seesCiv);

                aWorldState.Set(HalfZombeeScenario.HeardUnpleasantNoise, heardUnpleasantNoise);

                aWorldState.Set(HalfZombeeScenario.SeesLight, seesLight);

                aWorldState.Set(HalfZombeeScenario.Dead, dead);
            }
        }

        #endregion

        #region Infected

        public Infected infected;

        #endregion

        #region Hearing

        public Hearing hearing;

        #endregion

        #region Health

        public Health health;

        public void Death()
        {
            health.HealthReducedToZeroEvent -= Death;
            beeWings.DeleteWings();
            dead = true;
        }

        #endregion

        #region Vision

        public OscarVision vision;

        #endregion

        #region Pathfinding

        public PatrolPoint homePoint;

        public PatrolPoint prevPoint;

        #endregion

        #region Wings

        public BeeWingsManager beeWings;

        #endregion
    }
}