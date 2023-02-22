using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamGuySensor2 : MonoBehaviour, ISense
{
	public bool hasThings;
	public bool hasStuffs;

	public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
	{
		aWorldState.BeginUpdate(aAgent.planner);
		aWorldState.Set("hasThings", hasThings);
		aWorldState.Set("hasStuffs", hasStuffs);
		aWorldState.EndUpdate();
	}
}
