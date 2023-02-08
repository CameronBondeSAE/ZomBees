using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AntAIState
{
	public override void Execute(float aDeltaTime, float aTimeScale)
	{
		base.Execute(aDeltaTime, aTimeScale);
		
		transform.Rotate(0, 10f * Time.deltaTime, 0);
		
	}
}
