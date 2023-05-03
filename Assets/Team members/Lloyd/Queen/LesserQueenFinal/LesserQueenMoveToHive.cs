using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class LesserQueenMoveToHive : AntAIState
    {
        public LesserQueenSensor sensor;

        public Transform homePoint;

        public HalfZombeeTurnTowards turnTowards;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<LesserQueenSensor>();
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
        }

        public override void Enter()
        {
            base.Enter();
            homePoint = sensor.homePoint.transform;
            turnTowards.targetTransform = homePoint;
        }
    }
}