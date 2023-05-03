using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class HalfZombeeRunFromSound : AntAIState
    {
        public HalfZombeeProfile profile;

        public HalfZombeeSensor sensor;

        public Tether tether;

        public Rigidbody rb;

        public HalfZombeeTurnTowards turnTowards;

        public HalfZombeeTurnAway turnAway;

        public Hearing hearing;
        
        public HalfZombeePathfind pathfinder;

        public bool heardSomethingScary;
        
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            rb = aGameObject.GetComponent<Rigidbody>();
            tether = aGameObject.GetComponent<Tether>();
            sensor = aGameObject.GetComponent<HalfZombeeSensor>();
            profile = aGameObject.GetComponent<HalfZombeeProfile>();
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
            pathfinder = aGameObject.GetComponent<HalfZombeePathfind>();
            hearing = aGameObject.GetComponent<Hearing>();
            turnAway = aGameObject.GetComponent<HalfZombeeTurnAway>();
        }

        public override void Enter()
        {
            base.Enter();
            profile.currentSpeed = profile.walkSpeed;
            heardSomethingScary = true;

            turnTowards.enabled = false;
            turnAway.enabled = true;

            sensor.beeWings.ChangeBeeWingStats(-145, 25, true);

            Transform focusPoint = hearing.loudestRecentSound.Source.transform;

            turnAway.targetTransform = focusPoint.transform;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            heardSomethingScary = sensor.heardUnpleasantNoise;
            if(!heardSomethingScary)
                Finish();
        }

        public override void Exit()
        {
            base.Exit();
            turnTowards.enabled = true;
            turnAway.enabled = false;
        }
    }
}