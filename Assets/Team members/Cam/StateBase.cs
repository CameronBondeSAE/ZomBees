using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CameronBonde
{
	public class StateBase : MonoBehaviour
	{
		public StateManager stateManager;
		public int          aVariable;

		public void DoThing()
		{
			Debug.Log("CAM!");
		}
	}
}