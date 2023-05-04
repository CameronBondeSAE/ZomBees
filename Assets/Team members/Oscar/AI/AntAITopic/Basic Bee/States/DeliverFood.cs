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
	
	private bool finishDelivering;
	
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
	    finishDelivering = false;
	    FlightMode();
	    basicBeeControl.basicBeeWalking.SetActive(false); 
	    basicBeeControl.basicBeeFlying.SetActive(true); 
	    NavmeshEnabled();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
	    base.Execute(aDeltaTime, aTimeScale);

	    if (finishDelivering == false)
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
	    StartCoroutine(RunAway());
	    Exit();
    }
    
    IEnumerator RunAway()
    {
	    Vector3 position = PatrolManager.singleton
		    .sneaky[Random.Range(0, PatrolManager.singleton.sneaky.Count)].transform.position;
	    NavmeshFindLocation(position);
        
	    yield return new WaitForSeconds(5f);
	    finishDelivering = true;
        
	    Finish();
    }

    public override void Exit()
    {
	    base.Exit();

	    basicBeeControl.hasTheFood = false;
	    NavMeshFinish();
    }
}
