using System.Collections;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class LesserQueenAgitated : AntAIState
    {
        public float waitTime;

        private LesserQueenSensor sensor;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<LesserQueenSensor>();
        }

        public override void Enter()
        {
            base.Enter();
            StartCoroutine(WaitToReturn());
            sensor.agitated = true;
            sensor.beeWings.ChangeBeeWingStats(-90, 45, true);

        }

        private IEnumerator WaitToReturn()
        {
            yield return new WaitForSeconds(waitTime);
            sensor.agitated = false;
            Finish();
        }
    }
}