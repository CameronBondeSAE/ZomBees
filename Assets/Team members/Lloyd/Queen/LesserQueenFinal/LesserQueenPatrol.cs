using Anthill.AI;
using UnityEngine;

namespace Lloyd
{

    public class LesserQueenPatrol : AntAIState
    {
        public LesserQueenSensor queenSensor;
        public Hearing hearingComp;
        public QueenEvent queenEvent;
        public SphereBob bob;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            hearingComp = aGameObject.GetComponent<Hearing>();
            queenEvent = aGameObject.GetComponent<QueenEvent>();
            bob = aGameObject.GetComponent<SphereBob>();
        }

        public override void Enter()
        {
            base.Enter();
            bob.enabled = true;
            hearingComp.SoundHeardEvent += HeardSomething;
            queenEvent.OnChangeQueenState(LesserQueenState.Green);
        }

        public void HeardSomething(SoundProperties properties)
        {

        }

        public override void Exit()
        {
            bob.enabled = false;
            hearingComp.SoundHeardEvent -= HeardSomething;
        }
    }
}