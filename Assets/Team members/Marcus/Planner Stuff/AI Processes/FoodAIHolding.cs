using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FoodAIHolding : MonoBehaviour
    {
        public GameObject food;
        public bool holdingFood;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<Food>() && !holdingFood)
            {
                Destroy(collider.gameObject);
                GameObject myFood = Instantiate(food, gameObject.transform);
                
                myFood.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                myFood.transform.localPosition += new Vector3(-0.1f, -1f, 0);
                myFood.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

                holdingFood = true;
            }
        }
    }
}
