using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class HalfZombeeInvestigateNoise : AntAIState
    {
        public HalfZombeeProfile profile;

        public HalfZombeeSensor sensor;

        public HalfZombeeTurnTowards turnTowards;

        public CivVision vision;

        public Rigidbody rb;

        public Hearing hearing;

        public bool investigating;

        public HalfZombeePathfind pathfinder;

        public Tether tether;

        public Transform nextTarget;

        [Header("Min dist to noise to satisfy investigate")]
        public float minDist;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
            vision = aGameObject.GetComponent<CivVision>();
            profile = aGameObject.GetComponent<HalfZombeeProfile>();
            rb = aGameObject.GetComponent<Rigidbody>();
            hearing = aGameObject.GetComponent<Hearing>();
            pathfinder = aGameObject.GetComponent<HalfZombeePathfind>();
            tether = aGameObject.GetComponent<Tether>();
        }

        public override void Enter()
        {
            base.Enter();

            profile.currentSpeed = profile.runSpeed;

            if (hearing.soundsList.Any())
            {
                pathfinder.finalTarget = hearing.loudestRecentSound.Source.transform;
                pathfinder.SeekPath(pathfinder.patrolPoints);
            }

            investigating = true;
            StartCoroutine(PathFind());
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            if (!hearing.soundsList.Any())
            {
                investigating = false;
                Finish();
            }
        }

        private IEnumerator PathFind()
        {
            while (investigating)
            {
                if (pathfinder.lastViablePatrolPoint != null)
                {
                    turnTowards.targetPosition = pathfinder.lastViablePatrolPoint.transform.position;
                    tether.StartTether(rb, pathfinder.lastViablePatrolPoint.transform.position);
                    nextTarget = pathfinder.lastViablePatrolPoint.transform;

                    Debug.Log(Vector3.Distance(transform.position,
                        pathfinder.lastViablePatrolPoint.transform.position));

                    if (Vector3.Distance(transform.position, pathfinder.lastViablePatrolPoint.transform.position) <
                        minDist)
                        pathfinder.SeekPath(pathfinder.patrolPoints);

                    if ((Vector3.Distance(transform.position,
                            pathfinder.finalTarget.transform.position) < minDist))
                    {
                        Finish();
                    }
                }

                yield return null;
            }
        }

        public override void Exit()
        {
            base.Exit();
            investigating = false;
        }
    }
}