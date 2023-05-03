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

        public Hearing hearing;

        public Tether tether;

        [Header("Min dist to noise to satisfy investigate")]
        public float minDist;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<HalfZombeeSensor>();
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
            profile = aGameObject.GetComponent<HalfZombeeProfile>();
            hearing = aGameObject.GetComponent<Hearing>();
            tether = aGameObject.GetComponent<Tether>();
        }

        public override void Enter()
        {
            base.Enter();
            
            sensor.beeWings.ChangeBeeWingStats(-165, 7, true);

            profile.currentSpeed = profile.runSpeed;

            tether.homePoint = hearing.loudestRecentSound.Source.transform.position;

            turnTowards.targetTransform = hearing.loudestRecentSound.Source.transform;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            if (!hearing.soundsList.Any())
            {
                Finish();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}