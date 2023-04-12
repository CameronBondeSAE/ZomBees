using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oscar;

namespace Marcus
{
    public class FoodAIVision : MonoBehaviour
    {
        public int feelerAmount;
        public float feelerLength;

        private float spacing;
        private float offset;
        
        public List<GameObject> visableFood;
        public List<GameObject> visableObjects;

        private void Start()
        {
            visableFood = new List<GameObject>();
            visableObjects = new List<GameObject>();
            
            spacing = 20f / feelerAmount;
            offset = feelerAmount / 2f;

            StartCoroutine(UpdateVision());
        }

        public delegate void OnObjectSeen(GameObject thing);
        public event OnObjectSeen memoryEvent;
        
        private IEnumerator UpdateVision()
        {
            while (true)
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

                        if (hitInfo.collider.GetComponent<DynamicObject>() &&
                            !visableObjects.Contains(hitInfo.collider.gameObject))
                        {
                            visableObjects.Add(hitInfo.collider.gameObject);
                            memoryEvent?.Invoke(hitInfo.collider.gameObject);
                        }
                    }
                    else
                    {
                        visableFood.Clear();
                        visableObjects.Clear();
                    }
                }

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
