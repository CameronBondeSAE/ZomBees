using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using Sirenix.OdinInspector;
using SplineMesh;
using UnityEngine;
using Virginia;

namespace Johns
{
	public class GeneratorModel : DynamicObject, IItem, ISwitchable, IPowered
	{
		public float currFuel;
		public float maxFuel = 100;
		public List<IPowered> thingToGivePowerTo;
		public bool wasPowered;
		public Collider triggerBoxCollider;

		public void PoweredOn()
		{
			GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
			wasPowered = true;
		}

		public void PoweredOff()
		{
			GetComponent<StateManager>().ChangeState(GetComponent<GeneratorShuttingDownState>());
		}

		//just a check to see if it is at 0 fuel to power it off
		private void FixedUpdate()
		{
			if (currFuel <= 0 && wasPowered)
			{
				PoweredOff();
				wasPowered = false;
			}
		}

		//The thing that handles filling up the fuel
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

		//these 2 below are just to get a response from the switch
		public void TurnOn()
		{
			PoweredOn();
		}

		[Button]
		public void TurnOff()
		{
			PoweredOff();
		}

		public void Toggle()
		{
			throw new NotImplementedException();
		}
	}
}