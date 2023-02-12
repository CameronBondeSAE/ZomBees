using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float sightDistance;
    public bool wallSpotted;
    public LayerMask Wall;

    private void Update()
    {
        HandleSight();
    }

    private void HandleSight()
    {
        wallSpotted = false;
        RaycastHit hit;
        Debug.DrawRay(transform.localPosition, transform.forward * sightDistance, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hit, sightDistance, Wall))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            wallSpotted = true;
        }
    }
}