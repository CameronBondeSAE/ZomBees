using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Anthill.AI;
using UnityEngine;

namespace Marcus
{
    public class EatFood : AntAIState
    {
        private FoodAIHolding hand;
        private FoodAIHunger hunger;

        private float waitTimer = 3f;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            
            hand = aGameObject.GetComponentInChildren<FoodAIHolding>();
            hunger = aGameObject.GetComponent<FoodAIHunger>();
        }

        public override void Enter()
        {
            base.Enter();

            //send event to bobble food and make eating noises
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            waitTimer -= aDeltaTime;
            if (waitTimer <= 0)
            {
                hand.AteFood();
                hunger.AteFood();
            }
        }
    }
}
