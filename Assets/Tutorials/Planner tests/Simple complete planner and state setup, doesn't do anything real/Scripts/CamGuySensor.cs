using Anthill.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamGuySensor : MonoBehaviour, ISense
{
	public bool demoBoolFakeHasAmmo          = false;
	public bool demoBoolFakeHasTarget        = false;
	public bool demoBoolFakeDoDamageToPlayer = false;
	
	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		aWorldState.BeginUpdate(aAgent.planner);
		{
			aWorldState.Set(GuyScenario.HasAmmo, demoBoolFakeHasAmmo);
			aWorldState.Set(GuyScenario.HasTarget, demoBoolFakeHasTarget);
			aWorldState.Set(GuyScenario.DoDamageToPlayer, demoBoolFakeDoDamageToPlayer);
		}
		aWorldState.EndUpdate();

	}
}
