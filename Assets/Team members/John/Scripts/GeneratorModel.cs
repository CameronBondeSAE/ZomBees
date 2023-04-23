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
		public bool EnteredTrigger;
		public int currFuel = 0;
		public int maxFuel = 100;
		public int rateOfConsumption;
		public ISwitchable thingToGivePowerTo;

		
		public void TurnOn()
		{
			if (currFuel > 0)
			{
				GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
				StartCoroutine(FuelDrainCoroutine());
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
			if (other.tag == "Fuel")
			{
				Debug.Log("this works");
				EnteredTrigger = true;
				currFuel += other.gameObject.GetComponent<GasTank>().FuelAmount;
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
			yield return new WaitForSeconds(1);
			currFuel -= rateOfConsumption;
		}
	}
}