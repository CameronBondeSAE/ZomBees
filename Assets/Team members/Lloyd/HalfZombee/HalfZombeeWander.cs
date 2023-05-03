using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class HalfZombeeWander : AntAIState
    {
        public HalfZombeeProfile profile;

        public HalfZombeeSensor sensor;

        public Tether tether;

        public Rigidbody rb;

        public HalfZombeeTurnTowards turnTowards;

        public HalfZombeePathfind pathfinder;

        public Transform homePos;
        
        public Transform nextTarget;

        [Header("Min dist to noise to satisfy movement")]
        public float minDist;

        
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            rb = aGameObject.GetComponent<Rigidbody>();
            tether = aGameObject.GetComponent<Tether>();
            sensor = aGameObject.GetComponent<HalfZombeeSensor>();
            profile = aGameObject.GetComponent<HalfZombeeProfile>();
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
            pathfinder = aGameObject.GetComponent<HalfZombeePathfind>();
        }

        public override void Enter()
        {
            base.Enter();
            profile.currentSpeed = profile.walkSpeed;

            homePos = PatrolManager.singleton.hivePoints[0].transform;

            turnTowards.targetTransform = homePos;

            pathfinder.finalTarget = homePos;
            pathfinder.SeekPath(pathfinder.hivePoints);

            StartCoroutine(PathFind());
        }
        
        private IEnumerator PathFind()
        {
            while (true)
            {
                if (pathfinder.lastViablePatrolPoint != null)
                {
                    tether.StartTether(rb, pathfinder.lastViablePatrolPoint.transform.position);
                    nextTarget = pathfinder.lastViablePatrolPoint.transform;
                    turnTowards.targetTransform = nextTarget;

                    if (Vector3.Distance(transform.position, pathfinder.lastViablePatrolPoint.transform.position) <
                        minDist)
                        pathfinder.SeekPath(pathfinder.hivePoints);

                    if ((Vector3.Distance(transform.position,
                            homePos.position) < minDist))
                    {
                        nextTarget = homePos;
                        tether.StartTether(rb, homePos.position);
                        turnTowards.targetTransform = homePos;
                    }
                }

                yield return null;
            }
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            if (sensor.heardNoise || sensor.seesCiv)
                Finish();
        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}