using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

namespace Marcus
{
    public class FoodAIHolding : MonoBehaviour
    {
        public GameObject food;
        public bool holdingFood;

        // Create references to any items we can pick up
        public GameObject otherItem;
        private bool holdingItem;
        
        public OscarVision vision;

        private GameObject myItem;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<IItem>() != null && !holdingItem)
            {
                //Call Pickup on IItem
                PickUpItem(collider.gameObject);
            }
        }

        public void PickUpItem(GameObject item)
        {
            vision.objectsInSight.Remove(item);

            if (item.GetComponent<DynamicObject>().isFood)
            {
                vision.foodInSight.Remove(item);
                
                myItem = Instantiate(food, gameObject.transform);

                holdingFood = true;
            }
            else
            {
                myItem = Instantiate(otherItem, gameObject.transform);
            }
            myItem.transform.localPosition += new Vector3(-0.1f, -1f, 0.1f);
            
            holdingItem = true;
        }
        
        public void DropItem()
        {
            //Call Dispose
            holdingItem = false;
            holdingFood = false;
        }

        public void AteFood()
        {
            //Call Consume
            holdingItem = false;
            holdingFood = false;
        }
    }
}
