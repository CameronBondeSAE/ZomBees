using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Newtonsoft.Json.Schema;
using Oscar;
using UnityEngine;
using UnityEngine.AI;
using Virginia;
using Random = UnityEngine.Random;

public class DeliverFood : OscarsLittleGuyMovement
{
	private Inventory inventory;

	private void OnEnable()
	{
		objectArrivedEvent += arrivedAtLocation;
	}
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		inventory = aGameObject.GetComponent<Inventory>();
	}

	public override void Enter()
    {
	    base.Enter();
	    FlightMode();
	    NavmeshEnabled();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
	    base.Execute(aDeltaTime, aTimeScale);

	    if (inventory.heldItem != null)
	    {
		    NavmeshFindLocation(
			    PatrolManager.singleton.hivePoints[Random.Range(0, PatrolManager.singleton.hivePoints.Count)].transform.position);
	    }
    }

    public void arrivedAtLocation()
    {
	    LandMode();
	    inventory.Dispose();
	    Exit();
    }

    public override void Exit()
    {
	    base.Exit();
	    
	    NavMeshFinish();
    }
}
