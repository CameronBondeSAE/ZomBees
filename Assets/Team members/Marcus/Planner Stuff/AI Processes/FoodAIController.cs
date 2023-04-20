using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FoodAIController : MonoBehaviour
    {
        public OscarVision vision;
        public FoodAIHolding hand;
        public CivilianTraits hunger;
        public Hearing ears;

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
            return false;
            //return hunger.thresholdHit;
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

        public bool CanHearBee()
        {
            if (ears.heardSound)
            {
                foreach (SoundProperties sound in ears.soundsList)
                {
                    if (sound.Team == Team.Bee)
                        return true;
                }
            }

            return false;
        }
    }
}
