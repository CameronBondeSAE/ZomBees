using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Marcus
{
    public class FoodAIHolding : MonoBehaviour
    {
        public bool holdingFood;

        // Create references to any items we can pick up
        public GameObject otherItem;
        private bool holdingItem;
        
        public OscarVision vision;

        private GameObject myItem;

        private void OnTriggerEnter(Collider collider)
        {
            GameObject triggeredObject = collider.gameObject;
            
            if (triggeredObject.GetComponent<IItem>() != null && !holdingItem)
            {
                triggeredObject.GetComponent<IItem>().Pickup(gameObject);
                PickUpItem(triggeredObject);
            }
        }

        public void PickUpItem(GameObject item)
        {
            vision.objectsInSight.Remove(item);

            if (item.GetComponent<DynamicObject>().isFood)
            {
                vision.foodInSight.Remove(item);
                holdingFood = true;
            }
            myItem = Instantiate(otherItem, transform);
            
            myItem.GetComponent<Rigidbody>().isKinematic = true;
            myItem.GetComponent<Collider>().enabled = false;
            
            myItem.transform.localPosition += new Vector3(-0.1f, -1f, 0.1f);
            myItem.transform.localScale = Vector3.one / 2;
            myItem.transform.Rotate(0, 45, 0);

            holdingItem = true;
        }
        
        [Button]
        public void DropItem()
        {
            otherItem.GetComponent<IItem>().Dispose();
            
            holdingItem = false;
            holdingFood = false;
        }

        public void AteFood()
        {
            otherItem.GetComponent<IItem>().Consume();
            
            holdingItem = false;
            holdingFood = false;
        }
    }
}
