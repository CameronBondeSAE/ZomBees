using Anthill.AI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lloyd
{
    public class CivilianInteractState : CivModelAIState
    {
        public float radius;

        private Collider[] colliders = new Collider[10];
        private int numColliders;

        private bool wantToActivate;

       // private bool wantToPickUp;

        //change for ambidexterous
       // private GameObject rightHand;

       [Button]
        public override void Enter()
        {
         //   rightHand = civBrain.rightHand;

           // radius = civBrain.pickupRadius;
            
            numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);

            for (int i = 0; i < numColliders; i++)
            {
                /*ItemPickup pickup = colliders[i].GetComponent<ItemPickup>();
                if (pickup != null)
                {
                    if (wantToActivate)
                        pickup.SwitchActive();

                  //  else if (wantToPickUp)
                   // {
                    //    pickup.SwitchHeld(rightHand);
                  //  }
                }*/
                
                IInteractable interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }

            }
        }
    }
}