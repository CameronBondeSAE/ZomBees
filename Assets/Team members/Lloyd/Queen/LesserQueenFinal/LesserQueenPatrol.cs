using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Utilities;

namespace Lloyd
{
    //patrol 
    public class LesserQueenPatrol : AntAIState
    {
        public LesserQueenSensor queenSensor;

        //public SphereBobRB bob;
        public QueenEvent queenEvent;

        public HalfZombeeTurnTowards turnTowards;

        //public SharedMaterialChanger materialChanger;

        public bool hearSomething;
        public bool seeSomething;

        public PatrolPoint homePoint;

        public PatrolPoint currMoveTarget;
        public PatrolPoint previousMoveTarget;

        public float minDist;

        private List<PatrolPoint> hivePointsReference;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            queenEvent = aGameObject.GetComponent<QueenEvent>();
            queenSensor = aGameObject.GetComponent<LesserQueenSensor>();
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
            //materialChanger = aGameObject.GetComponentInChildren<SharedMaterialChanger>();

            // bob = aGameObject.GetComponent<SphereBobRB>();


            queenSensor.beeWings.ChangeBeeWingStats(-90, 15, true);

            hearSomething = false;
            seeSomething = false;
        }

        public override void Enter()
        {
            base.Enter();
//            bob.enabled = true;
            //queenEvent.OnChangeQueenState(LesserQueenState.Green);
//            materialChanger.ChangeColorGreen();

            hivePointsReference = PatrolManager.singleton.hivePoints;


            currMoveTarget = GetNewPatrolPoint();
            turnTowards.targetTransform = currMoveTarget.transform;

            if (queenSensor.homePoint == null)
            {
                queenSensor.homePoint = currMoveTarget;
            }

            queenSensor.patrol = true;
            
            homePoint = currMoveTarget;

            StartCoroutine(WaitUntilNewPoint());
        }

        private IEnumerator WaitUntilNewPoint()
        {
            while (true)
            {
                float distance = Vector3.Distance(transform.position, currMoveTarget.transform.position);
//                Debug.Log(distance);

                if (distance <= minDist)
                {
                    NewPatrolPoint();
                    yield break;
                }

                yield return null;
            }
        }

        public List<PatrolPoint> shuffledHivePoints;
        public int nextPatrolPointIndex = 0;

        private void ShuffleHivePoints()
        {
            shuffledHivePoints = new List<PatrolPoint>(hivePointsReference);

            for (int i = shuffledHivePoints.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                PatrolPoint temp = shuffledHivePoints[i];
                shuffledHivePoints[i] = shuffledHivePoints[j];
                shuffledHivePoints[j] = temp;
            }

            nextPatrolPointIndex = 0;
        }

        private PatrolPoint GetNewPatrolPoint()
        {
            if (shuffledHivePoints == null || nextPatrolPointIndex >= shuffledHivePoints.Count)
            {
                ShuffleHivePoints();
            }

            PatrolPoint newPatrolPoint = shuffledHivePoints[nextPatrolPointIndex];

            nextPatrolPointIndex++;

            previousMoveTarget = newPatrolPoint;
            queenSensor.previousPoint = newPatrolPoint;

            return newPatrolPoint;
        }

        private void NewPatrolPoint()
        {
            queenSensor.previousPoint = previousMoveTarget;
            currMoveTarget = GetNewPatrolPoint();
            turnTowards.targetTransform = currMoveTarget.transform;

            StartCoroutine(WaitUntilNewPoint());
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            hearSomething = queenSensor.heardSound;
            seeSomething = queenSensor.seesTarget;

            if (hearSomething || seeSomething)
            {
                Finish();
            }
        }

        public override void Exit()
        {
            // bob.enabled = false;
            StopAllCoroutines();
        }
    }
}