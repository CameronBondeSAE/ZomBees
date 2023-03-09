using UnityEngine;

namespace Johns
{
	public class GeneratorModel : MonoBehaviour, ISwitchable
	{
		public ISwitchable thingToGivePowerTo;

		public void TurnOn()
		{
			GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
		}

		public void TurnOff()
		{
		}
	}
}