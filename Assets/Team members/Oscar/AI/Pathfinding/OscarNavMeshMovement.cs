using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;
using UnityEngine.AI;

public class OscarNavMeshMovement : MonoBehaviour
{
    private LittleGuy littleGuy;
    
    private NavMeshAgent navMeshAgent;

    //private Transform target;
    private NavMeshPath path;
    
    private GameObject finalDestination;
    private float stoppingDistance = 1f;

    private void OnEnable()
    {
        path = new NavMeshPath();
    }

    private void Awake()
    {
        littleGuy = GetComponent<LittleGuy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    public void GoToDestination(GameObject destinationPos)
    {
        finalDestination = destinationPos;
        
        path.ClearCorners();
        NavMesh.CalculatePath(littleGuy.transform.position,finalDestination.transform.position,NavMesh.AllAreas, path);
    }

    private void Update()
    {
        if (finalDestination)
        {
            float distanceFromPoint = Vector3.Distance(littleGuy.transform.position, finalDestination.transform.position);
            
            if (distanceFromPoint <= stoppingDistance)
            {
                FinishUp();
            }
            littleGuy.rb.AddRelativeForce(Vector3.forward * littleGuy.speed);
            littleGuy.rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, 
                finalDestination.transform.position - transform.position, Vector3.up) * littleGuy.turnSpeed,0);
        }
    }

    void FinishUp()
    {
        
    }
}
