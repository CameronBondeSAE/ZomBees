using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsion : MonoBehaviour
{
    [SerializeField] private float overlapRadius = 5f;
    [SerializeField] private float maxRepulsionForce = 50f;
    [SerializeField] private float scanTime = 0.6f;
    
    private Vector3 repulsionForce = Vector3.zero;
    private Rigidbody rb;
    private Collider[] nearbyObjects = new Collider[10];

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RepulseSphere());
    }

    private IEnumerator RepulseSphere()
    {
        WaitForSeconds wait = new WaitForSeconds(scanTime);
        while (true)
        {
            int numNearbyObjects = Physics.OverlapSphereNonAlloc(transform.position, overlapRadius, nearbyObjects);
            for (int i = 0; i < numNearbyObjects; i++)
            {
                Rigidbody nearbyRigidbody = nearbyObjects[i].attachedRigidbody;
                if (nearbyRigidbody != null && nearbyRigidbody != rb)
                {
                    Vector3 offset = transform.position - nearbyRigidbody.transform.position;
                    offset.y = 0f; 
                    float sqrDistance = offset.sqrMagnitude;
                    float repulsionStrength = Mathf.Clamp01((overlapRadius * overlapRadius) / sqrDistance);
                    float repulsionForceMagnitude = Mathf.Min(repulsionStrength * maxRepulsionForce, maxRepulsionForce);
                    repulsionForce += offset.normalized * repulsionForceMagnitude;
                    nearbyRigidbody.AddForce(repulsionForce, ForceMode.Force);
                }
            }
            yield return wait;
        }
    }
}