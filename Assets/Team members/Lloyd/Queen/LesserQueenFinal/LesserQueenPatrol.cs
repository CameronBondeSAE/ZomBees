using Anthill.AI;
using UnityEngine;

namespace Lloyd
{

    public class LesserQueenPatrol : AntAIState
    {
        public LesserQueenSensor queenSensor;
        public SphereBobRB bob;
        public QueenEvent queenEvent;

        public SharedMaterialChanger materialChanger;

        public bool hearSomething;
        public bool seeSomething;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            queenEvent = aGameObject.GetComponent<QueenEvent>();
            queenSensor = aGameObject.GetComponent<LesserQueenSensor>();
            materialChanger = aGameObject.GetComponentInChildren<SharedMaterialChanger>();

           // bob = aGameObject.GetComponent<SphereBobRB>();
            
            queenSensor.beeWings.ChangeBeeWingStats(-90, 15, true);

            hearSomething = false;
            seeSomething = false;
        }

        public override void Enter()
        {
            base.Enter();
//            bob.enabled = true;
            queenEvent.OnChangeQueenState(LesserQueenState.Green);
//            materialChanger.ChangeColorGreen();
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
//            bob.enabled = false;
        }
    }
}