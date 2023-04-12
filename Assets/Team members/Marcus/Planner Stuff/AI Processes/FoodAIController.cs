using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FoodAIController : MonoBehaviour
    {
        public OscarVision vision;
        public FoodAIHolding hand;
        public FoodAIHunger hunger;

        public bool CanSeeFood()
        {
            if (vision.foodInSight.Count > 0)
            {
                return true;
            }
            
            return false;
        }

        public bool HasFood()
        {
            if (hand.holdingFood)
            {
                return true;
            }

            return false;
        }

        public bool IsHungry()
        {
            if (hunger.isHungy)
            {
                return true;
            }
            
            return false;
        }
    }
}
