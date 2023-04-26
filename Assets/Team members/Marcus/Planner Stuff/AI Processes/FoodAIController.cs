using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;

namespace Marcus
{
    public class FoodAIController : MonoBehaviour
    {
        public OscarVision vision;
        public FoodAIHolding hand;
        public Hearing ears;
        public CivilianTraits traits;
        
        public TraitScriptableObject fear;
        public TraitScriptableObject hunger;

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
            return traits.GetTrait(hunger).thresholdHit;
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
                    if (sound.SoundType == SoundEmitter.SoundType.Bee)
                        return true;
                }
            }

            return false;
        }

        public bool isScared()
        {
           return traits.GetTrait(fear).thresholdHit;
        }
    }
}
