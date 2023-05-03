using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class LesserQueenInvestigateSound : AntAIState
    {
        public LesserQueenSensor sensor;
        public HalfZombeeTurnTowards lookAt;
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<LesserQueenSensor>();
            lookAt = aGameObject.GetComponent<HalfZombeeTurnTowards>();
        }

        public override void Enter()
        {
            base.Enter();
            lookAt.targetTransform = sensor.target;
            sensor.beeWings.ChangeBeeWingStats(-90, 25, true);
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            if (sensor.seesTarget || !sensor.heardSound )
            {
                Finish();
            }
        }
    }
}