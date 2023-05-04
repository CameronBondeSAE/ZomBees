using Anthill.AI;
using UnityEngine;
using Oscar;
using Utilities;

namespace Lloyd
{
    public class BeeStingerSensor : DynamicObject, ISense
    {
        LesserQueenLookAt look;
        
        public Rigidbody rb;

        public Vector3 homePoint;
        public Vector3 originalHomepoint;

        public PatrolPoint hivePoint;

        public Transform viewTransform;

        public BeeStingAttack.BeeStingType myType;

        public BeeStingerBrain brain;

        public void SetHomePoint(Vector3 newPos)
        {
            homePoint = newPos;
            originalHomepoint = newPos;
        }

        private void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            brain = GetComponent<BeeStingerBrain>();

            health = GetComponent<Health>();
            health.HealthReducedToZeroEvent += Death;

            if (homePoint == null)
            {
                homePoint = transform.position;
                originalHomepoint = homePoint;
            }

            StartWings();

            isBee = true;

            look = GetComponent<LesserQueenLookAt>();

            description = "A strange creature that looks like a giant bee. It's dangerous...";

            importance = .7f;
        }

        public void Update()
        {
            seesTarget = brain.seesCiv;
            if (seesTarget)
                attackTarget = brain.ReturnNearestCiv();

            heardSound = brain.heardSound;
            if (heardSound)
                attackTarget = brain.ReturnNearestSound();
        }

        #region AntAI

        public bool attacking = false;
        public bool idle = false;
        public bool seesTarget = false;
        public bool heardSound = false;
        public bool sting = false;
        public bool dead = false;

        public bool hasResource;
        public bool backToOrigin;

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            {
                aWorldState.Set(BeeStingerScenario.Idle, idle);
                aWorldState.Set(BeeStingerScenario.SeesTarget, seesTarget);
                aWorldState.Set(BeeStingerScenario.HeardSound, heardSound);
                aWorldState.Set(BeeStingerScenario.Dead, dead);
                aWorldState.Set(BeeStingerScenario.Attack, attacking);

                aWorldState.Set(BeeStingerScenario.HasResource, hasResource);
                aWorldState.Set(BeeStingerScenario.BackToOrigin, backToOrigin);

                aWorldState.Set(BeeStingerScenario.Sting, sting);
            }
            aWorldState.EndUpdate();
        }

        #endregion

        #region Attack

        public Transform attackTarget;

        public void SetAttackTarget(Transform newTarget)
        {
            attackTarget = newTarget;
        }

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

        #region Resources

        public int maxResources;

        public int currentResources;

        public void ChangeResources(int amount)
        {
            currentResources += amount;

            if (currentResources <= 0)
            {
                currentResources = 0;
            }

            if (currentResources >= maxResources)
            {
                currentResources = maxResources;

                hivePoint = PatrolManager.singleton.paths[0];
                homePoint = hivePoint.transform.position;
            }
        }

        #endregion

        #region Wings

        public BeeWingsManager beeWings;

        private void StartWings()
        {
            beeWings = GetComponentInChildren<BeeWingsManager>();
            
            if (myType == BeeStingAttack.BeeStingType.Attack)
                beeWings.numberOfWings = 2;

            else
                beeWings.numberOfWings = 6;
            
            beeWings.SetWings();
        }

        public void ChangeWings(float angle, float speed, bool alive)
        {
            beeWings.OnChangeStatEvent(angle, speed, alive);
        }

        #endregion
    }
}