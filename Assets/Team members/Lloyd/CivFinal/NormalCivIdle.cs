using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class NormalCivIdle : AntAIState
    {
        private LesserQueenSensor sensor;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<LesserQueenSensor>();
        }

        public override void Enter()
        {
            base.Enter();
            sensor.patrol = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
        }
    }
}