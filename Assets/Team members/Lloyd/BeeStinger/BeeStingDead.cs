using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class BeeStingDead : AntAIState
    {
        public BeeGib beeGib;

        public BeeStingerSensor sensor;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<BeeStingerSensor>();
        }

        public override void Enter()
        {
            base.Enter();
            beeGib = GetComponentInChildren<BeeGib>();
            beeGib.DetermineGib(BeeGib.BeeType.Medium);
            DestroyImmediate(sensor.gameObject);
        }
    }
}
