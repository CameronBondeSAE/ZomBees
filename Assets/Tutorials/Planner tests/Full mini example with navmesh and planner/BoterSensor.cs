using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameronBonde
{
	public class BoterSensor : MonoBehaviour, ISense
	{
		public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
		{
			Debug.Log("Collect conditions");
		
			aWorldState.BeginUpdate(aAgent.planner);
			aWorldState.Set("Has target",                  aAgent.GetComponent<Boter_Model>().target != null);
			aWorldState.EndUpdate();
		}
	}
}