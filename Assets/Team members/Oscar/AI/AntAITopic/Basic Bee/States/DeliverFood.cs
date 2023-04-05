using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using UnityEngine.AI;

public class DeliverFood : AntAIState
{
    private LittleGuy littleGuy;
    private OscarControllerAI controllerAI;

    private GameObject myhome;
    
    private NavMeshAgent navMeshAgent;
    private float arrivedDistance = 1.5f;
    public bool IveDelivered;

    private Transform target;
    private NavMeshPath path;
    private float elapsed;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject); 
        littleGuy = aGameObject.GetComponent<LittleGuy>();
        myhome = aGameObject.GetComponent<LittleGuy>().myHome;
        navMeshAgent = aGameObject.GetComponentInParent<NavMeshAgent>();
        controllerAI = aGameObject.GetComponent<OscarControllerAI>();
        
        path = new NavMeshPath();
        elapsed = 0.0f;

        TheHive();
    }

    public Vector3 TheHive()
    {
        Vector3 finalDestination = myhome.transform.position;
        
        if (controllerAI.hasTheFood() == true)
        {
            navMeshAgent.SetDestination(finalDestination);
        }
        return finalDestination;
    }

    public bool ReachedDestinationOrFailed()
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
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Vector3 theHive = Vector3.zero;
        if (ReachedDestinationOrFailed())
        {
            theHive = TheHive();
            IveDelivered = true;
        }
        
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(littleGuy.transform.position, theHive, NavMesh.AllAreas, path);
        }

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i],path.corners[i + 1],Color.red, 1f);
        }
    }
}
