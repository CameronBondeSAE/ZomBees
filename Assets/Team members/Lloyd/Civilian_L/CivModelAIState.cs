using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class CivModelAIState : AntAIState
    {
        public CivilianBrain civBrain;

        public Rigidbody rb;

        public StatsComp stats;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            civBrain = aGameObject.GetComponent<CivilianBrain>();
            stats = aGameObject.GetComponent<StatsComp>();  
            rb = aGameObject.GetComponent<Rigidbody>();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}