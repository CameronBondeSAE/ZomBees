using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Marcus
{
    public class DropItem : AntAIState
    {
        private FoodAIHolding hand;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            
            hand = aGameObject.GetComponentInChildren<FoodAIHolding>();
        }

        public override void Enter()
        {
            base.Enter();
            
            hand.DropItem();
        }
    }
}
