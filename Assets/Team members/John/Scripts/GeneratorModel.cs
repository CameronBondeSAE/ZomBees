using System;
using System.Collections;
using Oscar;
using SplineMesh;
using UnityEngine;
using Virginia;

namespace Johns
{
	public class GeneratorModel : DynamicObject, IItem, ISwitchable
	{
		public int currFuel;
		public int maxFuel = 100;
		public int rateOfConsumption = 1;
		public ISwitchable thingToGivePowerTo;
		public bool wasPowered;

		
		public void TurnOn()
		{
			GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
			StartCoroutine(FuelDrainCoroutine());
			wasPowered = true;
		}

		private void FixedUpdate()
		{
			if (currFuel == 0 && wasPowered)
			{
				TurnOff();
				wasPowered = false;
			}
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
			if (other.GetComponent<GasTank>())
			{
				Debug.Log("this works");
				currFuel += other.gameObject.GetComponent<GasTank>().fuelAmount;
				print(currFuel);
				if (currFuel > maxFuel)
				{
					currFuel = maxFuel;
				}

				if (currFuel <= 0)
				{
					currFuel = 0;
				}
				
			}
		}

		IEnumerator FuelDrainCoroutine()
		{
			for (int i = currFuel; currFuel > 0; currFuel--)
			{
				Debug.Log(currFuel);
				yield return new WaitForSeconds(rateOfConsumption);
			}
		}
	}
}