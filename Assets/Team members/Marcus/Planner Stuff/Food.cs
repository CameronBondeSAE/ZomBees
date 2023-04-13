using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using UnityEngine.Serialization;

namespace Marcus
{
    public class Food : DynamicObject, IItem
    {
        public GameObject food;
        
        public void Consume()
        {
            Destroy(gameObject);
        }

        public void Dispose()
        {
            Instantiate(food, transform.position + Vector3.forward, Quaternion.identity);
            Destroy(gameObject);
        }

        public string Description()
        {
            throw new NotImplementedException();
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            if (whoPickedMeUp.GetComponent<FoodAIHolding>())
            {
                whoPickedMeUp.GetComponent<FoodAIHolding>().otherItem = food;
            }
            
            UtilityManager.DeleteAfterDelay(gameObject);
        }
    }
}
