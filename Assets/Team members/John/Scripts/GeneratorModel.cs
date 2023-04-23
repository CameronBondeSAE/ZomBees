using System;
using Oscar;
using SplineMesh;
using UnityEngine;

namespace Johns
{
	public class GeneratorModel : DynamicObject, IItem, ISwitchable
	{
		public bool EnteredTrigger;
		public int currFuel = 0;
		public int maxFuel = 100;
		public ISwitchable thingToGivePowerTo;

		public void TurnOn()
		{
			GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
		}

		public void TurnOff()
		{
			GetComponent<StateManager>().ChangeState(GetComponent<GeneratorShuttingDownState>());
		}

		public void   Consume()
		{
			
		}

		public void   Dispose()
		{
			
		}

		public string Description()
		{
			return "Generator";
		}

		public void   Pickup(GameObject whoPickedMeUp)
		{
			
		}

		public void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Fuel")
			{
				EnteredTrigger = true;
			}
		}
	}
}