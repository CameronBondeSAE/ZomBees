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
        public FoodAIHolding hand;
        public FoodAIHunger hunger;

        private float waitTimer = 3f;

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
