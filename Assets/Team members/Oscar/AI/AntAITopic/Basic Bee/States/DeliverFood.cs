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

	private OscarControllerAI basicBeeControl;

	private void OnEnable()
	{
		objectArrivedEvent += arrivedAtLocation;
	}
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		inventory = aGameObject.GetComponentInParent<Inventory>();
		basicBeeControl = aGameObject.GetComponent<OscarControllerAI>();
	}

	public override void Enter()
    {
	    base.Enter();
	    FlightMode();
	    basicBeeControl.basicBeeWalking.SetActive(false); 
	    basicBeeControl.basicBeeFlying.SetActive(true); 
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
	    basicBeeControl.basicBeeWalking.SetActive(true); 
	    basicBeeControl.basicBeeFlying.SetActive(false);
	    
	    inventory.Dispose();
	    Exit();
    }

    public override void Exit()
    {
	    base.Exit();
	    
	    NavMeshFinish();
    }
}
