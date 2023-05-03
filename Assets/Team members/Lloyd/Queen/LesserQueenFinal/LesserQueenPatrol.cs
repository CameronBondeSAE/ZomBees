using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Utilities;

namespace Lloyd
{
    public class LesserQueenPatrol : AntAIState
    {
        public LesserQueenSensor queenSensor;
        //public SphereBobRB bob;
        public QueenEvent queenEvent;

        public HalfZombeeTurnTowards turnTowards;

        public SharedMaterialChanger materialChanger;

        public bool hearSomething;
        public bool seeSomething;

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
           
           hivePointsReference = PatrolManager.singleton.hivePoints;

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

            currMoveTarget = GetNewPatrolPoint();
            turnTowards.targetTransform = currMoveTarget.transform;

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
            // Create a new list to store the shuffled hive points
            shuffledHivePoints = new List<PatrolPoint>(hivePointsReference);

            // Shuffle the list using the Fisher-Yates shuffle algorithm
            for (int i = shuffledHivePoints.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                PatrolPoint temp = shuffledHivePoints[i];
                shuffledHivePoints[i] = shuffledHivePoints[j];
                shuffledHivePoints[j] = temp;
            }

            // Reset the next patrol point index
            nextPatrolPointIndex = 0;
        }

        private PatrolPoint GetNewPatrolPoint()
        {
            // Check if the shuffled list is empty or if we have reached the end of the list
            if (shuffledHivePoints == null || nextPatrolPointIndex >= shuffledHivePoints.Count)
            {
                // Shuffle the list and reset the next patrol point index
                ShuffleHivePoints();
            }

            // Get the next patrol point from the shuffled list
            PatrolPoint newPatrolPoint = shuffledHivePoints[nextPatrolPointIndex];

            // Increment the next patrol point index
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

        /*private PatrolPoint GetNewPatrolPoint()
        {
            //must set fly points in main scene

            PatrolPoint newRandomPoint;
            
            do
            {
                newRandomPoint = hivePointsReference[Random.Range(0, hivePointsReference.Count)];
                
            } while (newRandomPoint == previousMoveTarget);

            previousMoveTarget = newRandomPoint;

            return newRandomPoint;
        }*/

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
StopAllCoroutines();
        }
    }
}