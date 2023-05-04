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

        public QueenEvent queenEvent;

        public CivVision vision;

        public Transform attackTarget;

        public Transform moveTarget;

        public PatrolPoint homePoint;

        public PatrolPoint previousPoint;
        
        #region ANTAI
        
        public bool seesTarget;

        public bool heardSound;

        public bool dead;

        public bool hasResource;

        public bool spawnFollowers;

        public bool agitated;

        public bool patrol;

        public float resourceCount;

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            {
                aWorldState.Set(LesserQueenScenario.SeesTarget, seesTarget);
                aWorldState.Set(LesserQueenScenario.HeardSound, heardSound);

                aWorldState.Set(LesserQueenScenario.Dead, dead);

                aWorldState.Set(LesserQueenScenario.HasResource, hasResource);
                aWorldState.Set(LesserQueenScenario.SpawnFollowers, spawnFollowers);
                
                aWorldState.Set(LesserQueenScenario.Agitated, agitated);
                
                aWorldState.Set(LesserQueenScenario.Patrol, patrol);

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
            vision = GetComponent<CivVision>();
            SetEyes();
            SetWings();

            queenEvent = GetComponent<QueenEvent>();

            StartCoroutine(AnnounceToFollowers());
        }

        private IEnumerator AnnounceToFollowers()
        {   
            while(patrol)
            {
                queenEvent.OnChangeSwarmPoint(transform);
                yield return new WaitForSeconds(updateSwarmTimer);
            }
        }

        private void Update()
        {
            if (patrol)
            {
                if (vision.civObjects.Any())
                {
                    seesTarget = true;
                    attackTarget = vision.ReturnNearestCiv();
                }

                if (hearing.heardSound)
                {
                    heardSound = true;
                    moveTarget = hearing.loudestRecentSound.Source.transform;
                }
            }
            else
            {
                heardSound = false;
                seesTarget = false;
            }
            

        }
    }
}