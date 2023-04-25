using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class NormalCivBrain : MonoBehaviour
    {
        public AntAIAgent antAIAgent;
	
        void Start()
        {
            antAIAgent.SetGoal("Survive");
            
            antAIAgent.worldState.BeginUpdate(antAIAgent.planner);
            
            antAIAgent.worldState.Set("InRange", false);
            // antAIAgent.worldState.Set("Has target",                  false);
            antAIAgent.worldState.EndUpdate();
        }
    }
}
