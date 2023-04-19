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
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Inventory>() != null)
            {
                Inventory inventory = collision.gameObject.GetComponent<Inventory>();
                if (inventory.hand != null)
                {
                    inventory.Pickup();
                }
            }
        }

        public string Description()
        {
            //its honey, idk what to place here
            return "Food";
        }

        public void Pickup(GameObject obj)
        {
            //just disable the object until needed later
            UtilityManager.DisableAfterDelay(obj);
        }

        public void Consume()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
