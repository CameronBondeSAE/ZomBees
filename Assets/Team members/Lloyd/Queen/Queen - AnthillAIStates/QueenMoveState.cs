using System;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{

    public class QueenMoveState : AntAIState
    {
        public GameObject target;
        public float minDistance = 1f;
        public float shootForce = 10f;
        public float interval = 0.6f;

        private Rigidbody rb;
        private bool hasArrived = false;

        private QueenScenarioManager queenScene;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            queenScene = aGameObject.GetComponent<QueenScenarioManager>();

            rb = queenScene.rb;

            distances = new List<Tuple<float, GameObject>>();

            DecideOnTarget();
        }

        private List<Tuple<float, GameObject>> distances;

        public void DecideOnTarget()
        {
            List<GameObject> targetList = queenScene.patrolPoints;

            distances.Clear();
            foreach (GameObject obj in targetList)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                distances.Add(new Tuple<float, GameObject>(distance, obj));
            }

            distances.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            List<GameObject> orderedObjects = new List<GameObject>();
            foreach (Tuple<float, GameObject> tuple in distances)
            {
                orderedObjects.Add(tuple.Item2);
            }

            GameObject closestObject = orderedObjects[0];

            ShootTowardsTarget(closestObject.transform.position);
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            if (!hasArrived)
            {
                Invoke("CheckDistanceToTarget", interval);
            }
        }

        private void ShootTowardsTarget(Vector3 targetPos)
        {
            if (!hasArrived)
            {
                Vector3 direction = (targetPos - transform.position).normalized;
                rb.AddForce(direction * shootForce, ForceMode.Impulse);
            }
        }

        private void CheckDistanceToTarget()
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                Debug.Log("Arrived at target!");
                hasArrived = true;
            }
        }
    }
}