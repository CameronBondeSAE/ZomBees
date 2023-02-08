using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerState : AntAIState
{
	public override void Enter()
	{
		base.Enter();
		
		Debug.Log("Shoot player");
	}

	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		
		
	}

	public override void Exit()
	{
		base.Exit();
	}
}
