using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class Neighbours : MonoBehaviour
    {
        public List<Transform> overallList = new List<Transform>();

        public List<Transform> beeList = new List<Transform>();
        public List<Transform> civList = new List<Transform>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!overallList.Contains(other.transform))
            {
                overallList.Add(other.transform);
            }
            
            if (other.GetComponent<DynamicObject>() != null)
            {
                if (other.GetComponent<DynamicObject>().isBee)
                {
                    if (!beeList.Contains(other.transform))
                    {
                        beeList.Add(other.transform);
                    }
                }
                if (other.GetComponent<DynamicObject>().isCiv)
                {
                    if (!civList.Contains(other.transform))
                    {
                        civList.Add(other.transform);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (overallList.Contains(other.transform))
            {
                overallList.Remove(other.transform);
            }
            
            if (other.GetComponent<DynamicObject>() != null)
            {
                if (beeList.Contains(other.transform))
                {
                    beeList.Remove(other.transform);
                }
                if (civList.Contains(other.transform))
                {
                    civList.Remove(other.transform);
                }
            }
        }
    }
}

