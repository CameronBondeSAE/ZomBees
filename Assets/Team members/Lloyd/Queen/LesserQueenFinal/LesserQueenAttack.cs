using System.Collections;
using Anthill.AI;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{
    public class LesserQueenAttack : AntAIState
    {
        public float attackTime;

        public LesserQueenSensor queenSensor;

        public QueenEvent queenEvent;

        public Transform attackTarget;

        public HalfZombeeTurnTowards turnTowards;

        public HalfZombeeMoveForwards moveForwards;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            queenSensor = aGameObject.GetComponent<LesserQueenSensor>();
            queenEvent = aGameObject.GetComponent<QueenEvent>();
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
            moveForwards = aGameObject.GetComponent<HalfZombeeMoveForwards>();
        }

        public override void Enter()
        {
            base.Enter();
            moveForwards.enabled = false;
            attackTarget = queenSensor.attackTarget;
            queenSensor.beeWings.ChangeBeeWingStats(-90, 50, true);
            turnTowards.targetTransform = attackTarget;
            
            {
                queenSensor.agitated = true;
                StartCoroutine(Attack());
            }
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            if(!queenSensor.seesTarget)
                Finish();
        }

        private IEnumerator Attack()
        {
            queenSensor.agitated = true;
            
            queenEvent.OnChangeSwarmPoint(queenSensor.attackTarget);

            yield return new WaitForSeconds(attackTime);

            moveForwards.enabled = true;
            Finish();
        }
        
    }
}