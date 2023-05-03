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
        }

        public override void Enter()
        {
            base.Enter();
            profile.currentSpeed = profile.walkSpeed;
            heardSomethingScary = true;


            Transform focusPoint = hearing.loudestRecentSound.Source.transform;

            turnTowards.targetTransform = focusPoint.transform;
            pathfinder.finalTarget = focusPoint.transform;
            pathfinder.SeekPath(pathfinder.patrolPoints);
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
            heardSomethingScary = false;
        }
    }
}