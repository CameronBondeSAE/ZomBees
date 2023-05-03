using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class LesserQueenSensor : MonoBehaviour, ISense
    {
        public int beeBullets;

        public QueenEvent queenEvent;

        public CivVision vision;

        public Transform target;
        
        #region ANTAI

        public bool patrol;

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

                aWorldState.Set(LesserQueenScenario.PrepareToAttack, prepareToAttack);
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

        public List<Follower> followers;

        public void AddFollower(Follower foll)
        {
            followers.Add(foll);
        }

        public int numSwarmers;

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

            queenEvent = GetComponent<QueenEvent>();

            StartCoroutine(AnnounceToFollowers());
        }

        private IEnumerator AnnounceToFollowers()
        {
            while(!attack)
            {
                queenEvent.OnChangeSwarmPoint(transform);
                yield return new WaitForSeconds(.6f);
            }
        }

        private void Update()
        {
            if (vision.civObjects.Any())
            {
                seesTarget = true;
                   target = vision.ReturnNearestCiv();
            }
            else seesTarget = false;

            if (hearing.heardSound)
            {
                heardSound = true;
                target = hearing.loudestRecentSound.Source.transform;
            }
            else heardSound = false;
        }
    }
}