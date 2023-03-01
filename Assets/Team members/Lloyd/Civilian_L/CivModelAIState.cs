using Anthill.AI;
using UnityEngine;

namespace Team_members.Lloyd.Civilian_L
{
    public class CivModelAIState : AntAIState
    {
        public CivilianBrain civBrain;

        public Rigidbody rb;

        public Stats stats;

        private void Awake()
        {
            civBrain = GetComponent<CivilianBrain>();
            rb = GetComponent<Rigidbody>();
            stats = GetComponent<Stats>();
        }

        public override void Enter()
        {
            if (civBrain == null)
                civBrain = GetComponentInParent<CivilianBrain>();

            if (rb == null)
                rb = GetComponentInParent<Rigidbody>();
            
            
            if (stats == null)
                stats = GetComponentInParent<Stats>();
        }
    }
}