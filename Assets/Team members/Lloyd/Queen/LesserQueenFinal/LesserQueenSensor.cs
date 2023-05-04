using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anthill.AI;
using UnityEngine;
using Utilities;

namespace Lloyd
{
    public class LesserQueenSensor : MonoBehaviour, ISense
    {
        public int beeBullets;

        public QueenEvent queenEvent;

        public CivVision vision;

        public Transform attackTarget;

        public Transform moveTarget;

        public PatrolPoint homePoint;

        public PatrolPoint previousPoint;
        
        #region ANTAI

        public bool patrol;

        public bool hasBullets;

        public bool seesTarget;

        public bool heardSound;

        public bool dead;

        public bool hasResource;

        public bool spawnHive;

        public bool spawnFollowers;

        public bool attack;

        public bool prepareToAttack;

        public bool agitated;

        public float resourceCount;

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            {
                aWorldState.Set(LesserQueenScenario.Patrol, patrol);
                aWorldState.Set(LesserQueenScenario.SeesTarget, seesTarget);
                aWorldState.Set(LesserQueenScenario.HeardSound, heardSound);

                aWorldState.Set(LesserQueenScenario.Dead, dead);

                aWorldState.Set(LesserQueenScenario.HasResource, hasResource);
                aWorldState.Set(LesserQueenScenario.SpawnHive, spawnHive);
                aWorldState.Set(LesserQueenScenario.SpawnFollowers, spawnFollowers);

                aWorldState.Set(LesserQueenScenario.HasBullets, hasBullets);
                aWorldState.Set(LesserQueenScenario.Attack, attack);
                aWorldState.Set(LesserQueenScenario.Agitated, agitated);
            }
            aWorldState.EndUpdate();
        }

        #endregion

        #region Eyes

        public LesserQueenLookAt look;

        private void SetEyes()
        {
            look = GetComponent<LesserQueenLookAt>();
        }

        #endregion

        #region Ears

        public Hearing hearing;

        #endregion

        #region Followers

        [Header("How long between Queen updating Followers on her position")]
        public float updateSwarmTimer;

        public List<Follower> followers;

        public void AddFollower(Follower foll)
        {
            followers.Add(foll);
        }

        public int numSwarmers;

        #endregion

        #region Health

        public Health health;

        public void Death()
        {
            
        }

        #endregion

        #region Idle

        public SphereBob bob;

        #endregion

        #region WINGS

        [Header("BEE WINGS")] public BeeWingsManager beeWings;

        private void SetWings()
        {
           // beeWings.GetComponentInChildren<BeeWingsManager>();
            beeWings.SetWings();
        }

        #endregion

        public void OnEnable()
        {
            hearing = GetComponent<Hearing>();
            bob = GetComponent<SphereBob>();
            vision = GetComponent<CivVision>();
            SetEyes();
            SetWings();

            hasBullets = true;
            spawnFollowers = false;

            queenEvent = GetComponent<QueenEvent>();

            StartCoroutine(AnnounceToFollowers());
        }

        private IEnumerator AnnounceToFollowers()
        {   
            while(!attack)
            {
                queenEvent.OnChangeSwarmPoint(transform);
                yield return new WaitForSeconds(updateSwarmTimer);
            }
        }

        private void Update()
        {
            if (!agitated)
            {
                if (vision.civObjects.Any())
                {
                    seesTarget = true;
                    attackTarget = vision.ReturnNearestCiv();
                }
                else seesTarget = false;

                if (hearing.heardSound)
                {
                    heardSound = true;
                    moveTarget = hearing.loudestRecentSound.Source.transform;
                }
                else heardSound = false;
            }
        }
    }
}