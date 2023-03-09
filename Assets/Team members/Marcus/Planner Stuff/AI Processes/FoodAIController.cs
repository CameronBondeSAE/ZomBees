using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FoodAIController : MonoBehaviour
    {
        public FoodAIVision vision;
        public FoodAIHolding hand;

        public bool CanSeeFood()
        {
            if (vision.visableFood.Count > 0)
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
            return false;
        }
    }
}
