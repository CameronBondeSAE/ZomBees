using Oscar;
using UnityEngine;

namespace Johns
{
	public class GeneratorModel : DynamicObject, IItem, ISwitchable
	{
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
	}
}