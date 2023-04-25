using Anthill.AI;
using UnityEngine;

namespace Lloyd
{

    public class LesserQueenPatrol : AntAIState
    {
        public LesserQueenSensor queenSensor;
        public SphereBob bob;
        public QueenEvent queenEvent;

        public bool hearSomething;
        public bool seeSomething;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            queenEvent = aGameObject.GetComponent<QueenEvent>();
            queenSensor = aGameObject.GetComponent<LesserQueenSensor>();

            hearSomething = false;
            seeSomething = false;
        }

        public override void Enter()
        {
            base.Enter();
            bob = queenSensor.bob;
            bob.enabled = true;
            queenEvent.OnChangeQueenState(LesserQueenState.Green);
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            hearSomething = queenSensor.heardSound;
            seeSomething = queenSensor.seesTarget;
            
            if (hearSomething || seeSomething)
            {
                queenSensor.patrol = false;
                Finish();
            }
        }

        public override void Exit()
        {
          //  bob.enabled = false;
        }
    }
}