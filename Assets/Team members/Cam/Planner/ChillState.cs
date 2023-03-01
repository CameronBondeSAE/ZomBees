using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chill : AntAIState
{
	public CamModel camModel;
	
	public override void Create(GameObject aGameObject)
	{
		base.Create(aGameObject);

		camModel = aGameObject.GetComponent<CamModel>();
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}
}
