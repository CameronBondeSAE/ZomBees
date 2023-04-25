using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{

    public class QueenLerpTowards : AntAIState
    {
        // Queen has a List of GameObjects flyPoints which is as large as numFlyPoints
        // Queen get s a reference to the next flyPoint and stores it as currentFlyPoint, and the old previousFlyPoint

        // Queen uses LerpTowards to move itself to the currPoint. when it is within the float minDist, it changes points

        // multiple Lists for variable paths?

        private float flyTime;

        public List<GameObject> flyPoints;

        public GameObject prevFlyPoint;

        [ReadOnly] public GameObject currFlyPoint;

        public float curSpeed;
        [ReadOnly] public float maxSpeed;
        private float minDist = 1f;
        [ReadOnly] private bool isMoving = false;

        public QueenScenarioManager queenScene;

        public Rigidbody rb;

        public LookAtTarget lookAt;

        public bool interrupted = false;

        private QueenScenarioManager.QueenStates currstate;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            queenScene = aGameObject.GetComponent<QueenScenarioManager>();
            lookAt = queenScene.lookAt;
            rb = queenScene.rb;
            currstate = queenScene.currState;
            maxSpeed = queenScene.flySpeed;
        }

        public override void Enter()
        {
            flyPoints = new List<GameObject>(queenScene.patrolPoints);
            currFlyPoint = flyPoints[0];

            //ChooseNewFlyPoint();

            isMoving = true;
            StartCoroutine(MoveTowards(currFlyPoint.transform.position));

            lookAt.SetTarget(currFlyPoint.transform);

        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            curSpeed = rb.velocity.magnitude;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);


            if (interrupted)
                isMoving = false;
        }

        private IEnumerator MoveTowards(Vector3 newFlyPoint)
        {
            while (isMoving)
            {
                float journeyLength = Vector3.Distance(transform.position, newFlyPoint);
                while (!Mathf.Approximately(journeyLength, 0f) && journeyLength > minDist)
                {
                    Vector3 direction = (newFlyPoint - transform.position).normalized;
                    float distance = Vector3.Distance(transform.position, newFlyPoint);

                    float forceMagnitude = Mathf.Clamp(distance / Time.deltaTime, flyTime, maxSpeed);
                    Vector3 force = direction * forceMagnitude;

                    rb.AddForce(force, ForceMode.VelocityChange);

                    yield return null;
                    journeyLength = Vector3.Distance(transform.position, newFlyPoint);
                }

                if (flyPoints.Count > 1)
                {
                    int currIndex = flyPoints.IndexOf(currFlyPoint);
                    currFlyPoint = flyPoints[(currIndex + 1) % flyPoints.Count];
                }
            }

            queenScene.hasArrived = true;
        }

        private void ChooseNewFlyPoint()
        {
            int index = Random.Range(0, flyPoints.Count);
            while (flyPoints[index] == prevFlyPoint)
            {
                index = Random.Range(0, flyPoints.Count);
            }

            prevFlyPoint = currFlyPoint;
            currFlyPoint = flyPoints[index];
        }

        public override void Exit()
        {
            flyPoints.Clear();
        }
    }
}