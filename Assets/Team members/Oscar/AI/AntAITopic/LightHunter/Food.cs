using System;
using System.ComponentModel;
using Oscar;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;
using Virginia;

namespace Oscar
{
    public class Food : DynamicObject, IItem
    {
        public string Description()
        {
            //its honey, idk what to place here
            return description;
        }

        public void Pickup(GameObject obj)
        {
            //just disable the object until needed later
            
            UtilityManager.DisableAfterDelay(gameObject,obj.GetComponent<Inventory>().hand.gameObject);
        }

        public void Consume()
        {
            
        }

        public void Dispose()
        {
            UtilityManager.EnableAfterDelay(gameObject);
        }
    }
}
