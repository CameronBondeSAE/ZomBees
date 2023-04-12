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
        
        public OscarVision vision;

        private GameObject myFood;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<Food>() && !holdingFood)
            {
                vision.foodInSight.Remove(collider.gameObject);
                UtilityManager.DeleteAfterDelay(collider.gameObject);
                
                myFood = Instantiate(food, gameObject.transform);
                myFood.transform.localPosition += new Vector3(-0.1f, -1f, 0.1f);

                holdingFood = true;
            }
        }

        public void AteFood()
        {
            Destroy(myFood);
            holdingFood = false;
        }
    }
}
