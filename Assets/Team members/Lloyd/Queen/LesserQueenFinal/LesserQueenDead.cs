using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class LesserQueenDead : AntAIState
    {
        public BeeGib beeGib;

        public LesserQueenSensor sensor;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<LesserQueenSensor>();
        }

        public override void Enter()
        {
            base.Enter();
            beeGib = GetComponentInChildren<BeeGib>();
            beeGib.DetermineGib(BeeGib.BeeType.Large);
            DestroyImmediate(sensor.gameObject);
        }
    }
}