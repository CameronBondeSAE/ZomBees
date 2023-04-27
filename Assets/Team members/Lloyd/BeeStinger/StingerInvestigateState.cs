using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class StingerInvestigateState : AntAIState
    {
        public BeeStingerSensor sensor;

        public LesserQueenLookAt look;

        public SphereBob bob;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<BeeStingerSensor>();
            look = aGameObject.GetComponent<LesserQueenLookAt>();
            bob = aGameObject.GetComponent<SphereBob>();
        }

        public override void Enter()
        {
            base.Enter();
            look.target = sensor.attackTarget;
            bob.enabled = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            if (!sensor.heardSound || sensor.seesTarget)
            {
                bob.enabled = false;
                Finish();
            }
}
    }
}
