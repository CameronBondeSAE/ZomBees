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
        public bool holdingItem;
        
        public OscarVision vision;

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
            if (item.GetComponent<DynamicObject>().isFood)
            {
                holdingFood = true;
            }
            otherItem.transform.localPosition = transform.position + new Vector3(-0.1f, 0, 0.1f);
            otherItem.transform.localScale = Vector3.one / 2;
            otherItem.transform.Rotate(0, 45, 0);

            holdingItem = true;
        }
        
        [Button]
        public void DropItem()
        {
            otherItem.transform.position = transform.position + transform.forward;
            otherItem.transform.localScale = Vector3.one;

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
