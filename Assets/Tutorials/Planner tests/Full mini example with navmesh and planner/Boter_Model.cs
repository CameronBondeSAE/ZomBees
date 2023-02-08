using Anthill.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CameronBonde
{
	public class Boter_Model : MonoBehaviour
	{
		public AntAIAgent antAIAgent;

		public WaterTarget target;
	
		// Start is called before the first frame update
		void Start()
		{
			// You can set a lot of the Planner in code as well as the graphical editor
			antAIAgent.SetGoal("Arrive at target");
		
			// You need to tell AntAI that you're about to update the world states (conditions)
			// Mainly for speed, because every time you update the worldstates it triggers a new plan, so if you were updating 100 it would be SLOW
			antAIAgent.worldState.BeginUpdate(antAIAgent.planner);
			antAIAgent.worldState.Set("Am I at the target position", false);
			// antAIAgent.worldState.Set("Has target",                  false);
			antAIAgent.worldState.EndUpdate();
		}
	}
}