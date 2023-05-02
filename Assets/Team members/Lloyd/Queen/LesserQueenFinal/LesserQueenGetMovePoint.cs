using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Lloyd
{
    public class LesserQueenGetMovePoint : MonoBehaviour
    {
        public PatrolManager patrolManager;

        public List<PatrolPoint> flyPoints;

        public List<PatrolPoint> hivePoints;

        public Transform nearestPoint;

        private void Start()
        {
            patrolManager = PatrolManager.singleton;

            flyPoints = new List<PatrolPoint>(patrolManager.paths);
            hivePoints = new List<PatrolPoint>(patrolManager.pathsWithIndoors);
        }

        public void CalculateNearestPoint(List<PatrolPoint> inputList)
        {
            List<KeyValuePair<float, GameObject>> distances = new List<KeyValuePair<float, GameObject>>();
        
            foreach (PatrolPoint point in inputList)
            {
                float distance = Vector3.Distance(transform.position, point.transform.position);
                distances.Add(new KeyValuePair<float, GameObject>(distance, point.gameObject));
            }

            distances.Sort((a, b) => a.Key.CompareTo(b.Key));

            if (distances.Count > 0)
            {
                nearestPoint = distances[0].Value.transform;
            }
        }
    }
}