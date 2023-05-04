using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Oscar
{
    public class GetFood : OscarsLittleGuyMovement
    {
        private OscarVision vision;

        private Inventory inventory;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            inventory = aGameObject.GetComponentInParent<Inventory>();
            
            vision = aGameObject.GetComponentInChildren<OscarVision>();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            if (vision.foodInSight.Count > 0)
            {
                float distance = Vector3.Distance(littleGuy.transform.position, vision.foodInSight[0].transform.position);

                if (distance < 2f)
                {
                    inventory.Pickup();
                }

                TurnTowards(vision.foodInSight[0].transform.position);

                BasicMovement(10f);
            }
        }
    }
}