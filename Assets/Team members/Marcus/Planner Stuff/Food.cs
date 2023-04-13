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
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Collider>().enabled = true;
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
            
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
