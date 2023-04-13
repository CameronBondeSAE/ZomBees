using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

namespace Marcus
{
    public class Food : DynamicObject, IItem
    {
        public GameObject realFood;
        public GameObject heldFood;
        
        public void Consume()
        {
            Destroy(gameObject);
        }

        public void Dispose()
        {
            Instantiate(realFood, transform.position + Vector3.forward, Quaternion.identity);
            Destroy(gameObject);
        }

        public string Description()
        {
            throw new NotImplementedException();
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            UtilityManager.DeleteAfterDelay(gameObject);
        }
    }
}
