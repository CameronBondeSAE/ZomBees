using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class NormalCivInteract : AntAIState
    {
        public float radius;

        private Collider[] colliders = new Collider[10];
        private int numColliders;

        private bool wantToActivate;

        private CivSensor sensor;

        // private bool wantToPickUp;

        //change for ambidexterous
        // private GameObject rightHand;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<CivSensor>();
        }

        public override void Enter()
        {
            //   rightHand = civBrain.rightHand;

            // radius = civBrain.pickupRadius;

            numColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);

            for (int i = 0; i < numColliders; i++)
            {
                IInteractable interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    sensor.wantsToInteract = false;
                    Finish();
                }

            }
        }
    }
}
