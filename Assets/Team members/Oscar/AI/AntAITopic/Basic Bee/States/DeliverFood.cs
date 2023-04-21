using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Newtonsoft.Json.Schema;
using Oscar;
using UnityEngine;
using UnityEngine.AI;
using Virginia;

public class DeliverFood : OscarsLittleGuyMovement
{
	private Inventory inventory;

	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);
		inventory = aGameObject.GetComponent<Inventory>();
	}

	public override void Enter()
    {
	    base.Enter();
	    NavmeshEnabled();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
	    base.Execute(aDeltaTime, aTimeScale);

	    if (inventory.heldItem != null)
	    {
		    NavmeshFindLocation(littleGuy.myHome.transform.position);
	    }
    }

    public override void Exit()
    {
	    base.Exit();
	    
	    NavMeshFinish();
    }
}
