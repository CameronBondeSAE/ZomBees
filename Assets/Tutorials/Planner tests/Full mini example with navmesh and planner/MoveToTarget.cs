using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CameronBonde
{
	public class MoveToTarget : AntAIState
	{
		public GameObject owner;
		NavMeshAgent      navMeshAgent;

		public override void Create(GameObject aGameObject)
		{
			base.Create(aGameObject);

			owner        = aGameObject;
			navMeshAgent = owner.GetComponent<NavMeshAgent>();
		}

		public override void Enter()
		{
			base.Enter();

			Debug.Log("ENTER MOVE STATE");

			navMeshAgent.SetDestination(owner.GetComponent<Boter_Model>().target.transform.position);
		}

		public override void Execute(float aDeltaTime, float aTimeScale)
		{
			base.Execute(aDeltaTime, aTimeScale);

			// Have we got to the target?
			if (navMeshAgent.remainingDistance < 1f)
			{
				owner.GetComponent<Boter_Model>().target = null;
				Finish();
			}
		}
	}

}