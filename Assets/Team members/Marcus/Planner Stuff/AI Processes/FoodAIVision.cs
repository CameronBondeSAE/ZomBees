using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class FoodAIVision : MonoBehaviour
    {
        public int feelerAmount;
        public float feelerLength;

        private float spacing;
        private float offset;
        
        public List<object> visableFood;

        private void Start()
        {
            visableFood = new List<object>();
            
            spacing = 20f / feelerAmount;
            offset = feelerAmount / 2f;
        }

        private void Update()
        {
            for (int i = 0; i < feelerAmount; i++)
            {
                Vector3 scanDir = Quaternion.Euler(0,  i * spacing - offset, 0) * transform.forward;
                Ray visionLine = new Ray(transform.position, scanDir);
                RaycastHit hitInfo;

                if (Physics.Raycast(visionLine, out hitInfo, feelerLength))
                {
                    if (hitInfo.collider.GetComponent<Food>() != null && !visableFood.Contains(hitInfo.collider.gameObject))
                    {
                        visableFood.Add(hitInfo.collider.gameObject);
                    }
                }
                else
                {
                    visableFood.Clear();
                }
            }
        }
    }
}
