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
        public HearingComp ears;

        public bool CanSeeFood()
        {
            return vision.foodInSight.Count > 0;
        }

        public bool HasFood()
        {
            return hand.holdingFood;
        }

        public bool IsHungry()
        {
            return hunger.isHungy;
        }

        public bool HasItem()
        {
            return hand.holdingItem;
        }

        public bool CanSeeBee()
        {
            return vision.beesInSight.Count > 0;
        }

        public bool Day()
        {
            return WorldTime.Instance.isDay;
        }

        /*public bool CanHearBee()
        {
            // Return if I can hear a bee sound
            
        }*/
    }
}
