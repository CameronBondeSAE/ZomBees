using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Utilities;
//this is copy/pasted from LessQueenPatrol.cs

namespace Lloyd {
    public class HalfZombeeWander : AntAIState 
    {
        public HalfZombeeSensor sensor;

        public HalfZombeeTurnTowards turnTowards;

        //public SharedMaterialChanger materialChanger;

        public bool hearSomething;
        public bool seeSomething;

        public PatrolPoint homePoint;

        public PatrolPoint currMoveTarget;
        public PatrolPoint previousMoveTarget;

        public float minDist;

        private List<PatrolPoint> pathPointsReference;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            
            sensor = aGameObject.GetComponent<HalfZombeeSensor>();
            turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();

            hearSomething = false;
            seeSomething = false;
        }

        public override void Enter()
        {
            base.Enter();
            
            sensor.beeWings.ChangeBeeWingStats(-165, 3, true);
            
            pathPointsReference = PatrolManager.singleton.paths;
            currMoveTarget = GetNewPatrolPoint();
            turnTowards.targetTransform = currMoveTarget.transform;

            if (sensor.homePoint == null)
            {
                sensor.homePoint = currMoveTarget;
            }

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
            shuffledHivePoints = new List<PatrolPoint>(pathPointsReference);

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
            sensor.prevPoint = newPatrolPoint;

            return newPatrolPoint;
        }

        private void NewPatrolPoint()
        {
            sensor.prevPoint = previousMoveTarget;
            currMoveTarget = GetNewPatrolPoint();
            turnTowards.targetTransform = currMoveTarget.transform;

            StartCoroutine(WaitUntilNewPoint());
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            hearSomething = sensor.heardNoise;
            seeSomething = sensor.seesCiv;

            if (hearSomething || seeSomething)
            {
                Finish();
            }
        }

        public override void Exit()
        {
            StopAllCoroutines();
        }
    }
}
