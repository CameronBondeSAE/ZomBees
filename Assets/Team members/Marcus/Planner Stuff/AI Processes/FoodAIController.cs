using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FoodAIController : MonoBehaviour
    {
        public FoodAIVision vision;

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
            return false;
        }

        public bool IsHungry()
        {
            return false;
        }
    }
}
