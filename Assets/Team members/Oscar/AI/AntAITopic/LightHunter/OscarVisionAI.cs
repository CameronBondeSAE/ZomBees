using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using UnityEngine.Serialization;

public class OscarVisionAI : MonoBehaviour
{
    public List<GameObject> lightInSight;

    public List<GameObject> honeyInSight;

    public List<GameObject> civiliansVisible;

    public List<GameObject> hivesInSight;

    private void OnTriggerEnter(Collider other)
    {
        //if more things for vision look at 
        if (other != null)
        {
            if (other.GetComponent<DynamicObject>() != null)
            {
                if (other.GetComponent<DynamicObject>().isLit == true)
                {
                    GameObject litObj = other.gameObject;
                    
                    if (!lightInSight.Contains(litObj)) 
                    { 
                        lightInSight.Add(litObj); 
                    }
                }
            }
            
            if (other.GetComponent<PlayerBase>() != null)
            {
                GameObject civGuy = other.gameObject;
                    
                if (!civiliansVisible.Contains(civGuy))
                {
                    civiliansVisible.Add(civGuy);
                }
            }

            
            if (other.GetComponent<Food>() != null)
            {
                if (other.gameObject.GetComponent<Food>())
                {
                    GameObject honeyStuff = other.gameObject;

                    if (!honeyInSight.Contains(honeyStuff))
                    {
                        honeyInSight.Add(honeyStuff);
                    }
                }
            }

            if (other.GetComponent<Hive>() != null)
            {
                GameObject hiveLoc = other.gameObject;

                if (!hivesInSight.Contains(hiveLoc))
                {
                    hivesInSight.Add(hiveLoc);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<DynamicObject>() != null)
        {
            GameObject litObj = other.gameObject;
            
            lightInSight.Remove(litObj);
        }
        if (other.GetComponent<Food>() != null)
        {
            GameObject honeyStuff = other.gameObject;
        
            honeyInSight.Remove(honeyStuff);
        }
        if (other.GetComponent<PlayerBase>() != null)
        {
            GameObject civGuy = other.gameObject;
                
            civiliansVisible.Remove(civGuy);
        }
        if (other.GetComponent<Hive>() != null)
        {
            GameObject hiveLoc = other.gameObject;

            hivesInSight.Remove(hiveLoc);
        }
    }
}
