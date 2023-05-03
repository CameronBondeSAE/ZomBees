using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;

namespace Lloyd
{
    public class NormalCivPathFinder : MonoBehaviour
    {
        public List<GameObject> safePoints;
        public Transform nearestSafePoint;

        public List<GameObject> dangerousPoints;

        public List<GameObject> resourcePoints;

        public List<GameObject> interactPoints;
        public Transform nearestInteractPoint;

        public List<GameObject> civPoints;

        public List<
            PatrolPoint> patrolPoints;
    
        private float scanRadius = 10f;
        private LayerMask scanLayerMask;

        [Button]
        public void ScanNearby()
        {
            patrolPoints.Clear();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, scanRadius, scanLayerMask);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                PatrolPoint patrolPoint = hitColliders[i].GetComponent<PatrolPoint>();
                if (patrolPoint != null)
                {
                    patrolPoints.Add(patrolPoint);
                }
            }

            patrolPoints.Sort((a, b) => a.howHidden.CompareTo(b.howHidden));
        }

        public Transform CalculateNearestSafePoint()
        {
            float minDistance = float.MaxValue;

            foreach (GameObject obj in safePoints)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestSafePoint = obj.transform;
                }
            }
            return nearestSafePoint;
        }

        public Transform CalculateRandomSafePoint()
        {
            int randomIndex = UnityEngine.Random.Range(0, safePoints.Count);
            GameObject randomGameObject = safePoints[randomIndex];
            return randomGameObject.transform;
        }
    
        public Transform CalculateNearestInteractPoint()
        {
            float minDistance = float.MaxValue;

            foreach (GameObject obj in interactPoints)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestInteractPoint = obj.transform;
                }
            }
            return nearestInteractPoint;
        }
    }
}
