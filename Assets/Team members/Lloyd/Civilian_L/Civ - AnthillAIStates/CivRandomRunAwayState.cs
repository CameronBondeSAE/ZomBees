using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class CivRandomRunAwayState : AntAIState
{
    
    //cam stuff
    public NavMeshAgent navMeshAgent;
    public float arrivedDistance = 1.5f;

    // Debugging
    public Transform target;
    private NavMeshPath path;
    private float elapsed = 0.0f;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        navMeshAgent = aGameObject.GetComponent<NavMeshAgent>();
    }
    
    public override void Enter()
    {
        // Debugging
        path = new NavMeshPath();
        elapsed = 0.0f;

        FindRandomSpot();
    }


    [Button]
    public Vector3 FindRandomSpot()
    {
        if (PatrolManager.singleton.pathsWithIndoors.Count <= 0)
        {
            Debug.LogWarning("FindRandom no patrol points found");
            return Vector3.zero;
        }
        int index = 0;
        Vector3 finalTarget = Vector3.zero;
        bool foundTarget = false;

        // Find a non-null entry
        int bailOutCount = 100;
        while (PatrolManager.singleton.pathsWithIndoors[index] == null)
        {
            index = Random.Range(0, PatrolManager.singleton.pathsWithIndoors.Count);
            bailOutCount--;
            if (bailOutCount<=0)
            {
                Debug.LogWarning("FindRandom no patrol points found");
                return Vector3.zero;
            }
        }

        finalTarget = PatrolManager.singleton.pathsWithIndoors[index].transform.position;

        if (PatrolManager.singleton.pathsWithIndoors[index] != null)
        {
            navMeshAgent.SetDestination(finalTarget);
            return finalTarget;
        }

        Debug.LogWarning("FindRandom no patrol points found");
        return Vector3.zero; // HACK won't really know if it succeeded. Should be bool or something
    }


    public bool ReachedDestinationOrGaveUp()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + arrivedDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 randomSpot = Vector3.zero;
        if (ReachedDestinationOrGaveUp())
        {
            randomSpot = FindRandomSpot();
        }


        // Debugging
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, randomSpot, NavMesh.AllAreas, path);
        }

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 1f);
    }
}