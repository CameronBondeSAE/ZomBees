using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Newtonsoft.Json.Schema;
using Oscar;
using UnityEngine;
using UnityEngine.AI;

public class DeliverFood : OscarsLittleGuyMovement
{
	public override void Enter()
    {
	    base.Enter();
	    NavmeshEnabled();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
	    base.Execute(aDeltaTime, aTimeScale);
	    
	    NavmeshFindLocation(littleGuy.myHome.transform.position);
    }

    public override void Exit()
    {
	    base.Exit();
	    
	    NavMeshFinish();
    }
}
