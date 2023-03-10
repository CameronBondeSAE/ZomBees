using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FoodAIHunger : MonoBehaviour
    {
        public float baseHunger;
        public float hunger;
        public bool isHungy;

        private void Start()
        {
            AteFood();
        }

        private void Update()
        {
            hunger -= Time.deltaTime / 3;
            if (hunger < baseHunger / 5)
            {
                isHungy = true;
            }
        }

        public void AteFood()
        {
            hunger = baseHunger;
            isHungy = false;
        }
    }
}
