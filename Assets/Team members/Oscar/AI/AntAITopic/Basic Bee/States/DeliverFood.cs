using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Newtonsoft.Json.Schema;
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
    private Transform target;
    private NavMeshPath path;
    private float elapsed;
    private Vector3 finalDestination;
    private float stoppingDistance = 1f;
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
	    finalDestination = myhome.transform.position;
	    
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
	    
	    if (ReachedDestinationOrFailed())
	    {
		    finalDestination = TheHive();
	    }
	    
	    float distanceFromPoint = Vector3.Distance(littleGuy.transform.position, finalDestination);
	    if (distanceFromPoint <= stoppingDistance)
	    {
		    Finish();
		    
	    }

	    elapsed += Time.deltaTime;
	    if (elapsed > 1.0f)
	    {
		    elapsed -= 1.0f;
		    NavMesh.CalculatePath(littleGuy.transform.position, finalDestination, NavMesh.AllAreas, path);
	    }
	    
	    for (int i = 0; i < path.corners.Length - 1; i++)
	    {
		    Debug.DrawLine(path.corners[i],path.corners[i + 1],Color.red, 1f);
	    }
    }
    public override void Exit()
    {
	    base.Exit();
	    
	    navMeshAgent.enabled = false;
	    navMeshAgent.enabled = true;
    }
}
    
    /*private GameObject myhome;
    private OscarNavMeshMovement navMeshMovement;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        myhome = aGameObject.GetComponentInParent<LittleGuy>().myHome;
        navMeshMovement = aGameObject.GetComponentInParent<OscarNavMeshMovement>();
    }

    public override void Enter()
    {
        base.Enter();
        
        navMeshMovement.GoToDestination(myhome);
    }
}*/
