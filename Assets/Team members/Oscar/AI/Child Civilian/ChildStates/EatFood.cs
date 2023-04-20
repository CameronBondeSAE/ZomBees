using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Oscar
{
    public class EatFood : OscarsLittleGuyMovement
    {
        private Inventory inventory;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            inventory = aGameObject.GetComponent<Inventory>();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            inventory.Pickup();
            if (inventory.hand.GetComponent<DynamicObject>().isFood)
            {
                inventory.Consume();
            }
        }
    }
}