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

        private void OnEnable()
        {
            hand = GetComponentInParent<FoodAIHolding>();
            hunger = GetComponentInParent<FoodAIHunger>();
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
