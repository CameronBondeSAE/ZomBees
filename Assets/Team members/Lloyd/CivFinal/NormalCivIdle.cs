using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class NormalCivIdle : AntAIState
    {
        public LesserQueenSensor sensor;

        public QueenEvent queenEvent;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<LesserQueenSensor>();
            queenEvent = aGameObject.GetComponent<QueenEvent>();
        }

        public override void Enter()
        {
            base.Enter();
            queenEvent.OnChangeSwarmPoint(sensor.gameObject.transform);
            sensor.agitated = false;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
        }
    }
}