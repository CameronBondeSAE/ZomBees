using Anthill.AI;
using UnityEngine;

namespace Team_members.Lloyd.Civilian_L.Civ___AnthillAIStates
{
    public class CivilianInteractState : CivModelAIState
    {
        public float radius;

        private Collider[] colliders = new Collider[10];
        private int numColliders;

        private bool wantToActivate;

        private bool wantToPickUp;

        //change for ambidexterous
        private GameObject rightHand;

        public override void Enter()
        {
            rightHand = civBrain.rightHand;

            radius = civBrain.pickupRadius;

            wantToActivate = civBrain.wantToActivate;

            wantToPickUp = civBrain.wantToPickup;
            
            numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);

            for (int i = 0; i < numColliders; i++)
            {
                ItemPickup pickup = colliders[i].GetComponent<ItemPickup>();
                if (pickup != null)
                {
                    if (wantToActivate)
                        pickup.SwitchActive();

                    else if (wantToPickUp)
                    {
                        pickup.SwitchHeld(rightHand);
                    }
                }
            }
        }
    }
}