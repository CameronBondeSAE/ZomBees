using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

namespace CameronBonde
{
	public class PickARandomTarget : AntAIState
	{
		// Reference to my main GameObject, so I can access all the normal code I have in there.
		public GameObject owner;

		public override void Create(GameObject aGameObject)
		{
			base.Create(aGameObject);

			owner = aGameObject;
		}

		public override void Enter()
		{
			base.Enter();

			Debug.Log("Pick State");


			// HACK
			WaterTarget[] fakeTargets = FindObjectsOfType<WaterTarget>();

			// Pick a random target
			WaterTarget fakeTarget = fakeTargets[Random.Range(0, fakeTargets.Length)];

			if (fakeTarget != null)
			{
				owner.GetComponent<Boter_Model>().target = fakeTarget;
			}

			Finish();
		}
	}

}