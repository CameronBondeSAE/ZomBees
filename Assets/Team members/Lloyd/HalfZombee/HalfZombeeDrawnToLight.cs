using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anthill.AI;
using Oscar;
using UnityEngine;

namespace Lloyd
{
    public class HalfZombeeDrawnToLight : AntAIState
    {
        public HalfZombeeProfile profile;

        public HalfZombeeSensor sensor;

        public Tether tether;

        public Rigidbody rb;

        public HalfZombeeTurnTowards turnTowards;

        public OscarVision oscarVision;

        public HalfZombeePathfind pathfinder;

        public bool seesLight;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            rb = aGameObject.GetComponent<Rigidbody>();
            tether = aGameObject.GetComponent<Tether>();
            sensor = aGameObject.GetComponent<HalfZombeeSensor>();
            profile = aGameObject.GetComponent<HalfZombeeProfile>();
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
            pathfinder = aGameObject.GetComponent<HalfZombeePathfind>();
            oscarVision = aGameObject.GetComponent<OscarVision>();
        }

        public override void Enter()
        {
            base.Enter();
            profile.currentSpeed = profile.walkSpeed;

            Transform focusPoint = ReturnNearestLight();

            turnTowards.targetTransform = focusPoint.transform;
            pathfinder.finalTarget = focusPoint.transform;
            pathfinder.SeekPath(pathfinder.patrolPoints);
            seesLight = true;
        }

        public Transform ReturnNearestLight()
        {
            if (oscarVision.lightInSight.Any())
            {
                List<(float, Transform)> distanceAndTransformList = new List<(float, Transform)>();
                foreach (DynamicObject civ in oscarVision.lightInSight)
                {
                    float distance = Vector3.Distance(transform.position, civ.transform.position);
                    distanceAndTransformList.Add((distance, civ.transform));
                }

                distanceAndTransformList.Sort((a, b) => a.Item1.CompareTo(b.Item1));

                return distanceAndTransformList[0].Item2;
            }

            return null;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            seesLight = sensor.seesLight;
            if (!seesLight)
                Finish();
        }

        public override void Exit()
        {
            base.Exit();
            seesLight = false;
        }
    }
}