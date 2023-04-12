using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

public class OscarVision : MonoBehaviour
{
    public List<GameObject> beesInSight;

    public List<GameObject> foodInSight;

    public List<GameObject> civsInSight;

    public List<GameObject> objectsInSight;

    public delegate void OnObjectSeen(GameObject thing);
    public event OnObjectSeen memoryEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        //everything vision for anyone's use :D
        if (other != null)
        {
            if (other.GetComponent<LivingEntity>() != null)
            {
                //are they a Bee, use this:
                if (other.GetComponent<LivingEntity>().isBee == true)
                {
                    GameObject beeStuff = other.gameObject;
                    
                    if (!beesInSight.Contains(beeStuff))
                    {
                        beesInSight.Add(beeStuff);                    
                        
                        memoryEvent?.Invoke(beeStuff);
                    }
                }
                
                //are they a Civ, use this:
                if (other.GetComponent<LivingEntity>().isBee == false)
                {
                    GameObject civStuff = other.gameObject;

                    if (!civsInSight.Contains(civStuff))
                    {
                        civsInSight.Add(civStuff);

                        memoryEvent?.Invoke(civStuff);
                    }
                }
            }
            
            //is it food, use this:
            if (other.GetComponent<Food>() != null)
            {
                GameObject foodStuff = other.gameObject;
                
                if (!foodInSight.Contains(foodStuff))
                {
                    foodInSight.Add(foodStuff);
                    
                    memoryEvent?.Invoke(foodStuff);
                }
            }
            
            //is it a dynamicObject, use this:
            if (other.GetComponent<DynamicObject>() != null)
            {
                GameObject objectStuff = other.gameObject;

                if (!objectsInSight.Contains(objectStuff))
                {
                    objectsInSight.Add(objectStuff);
                    
                    memoryEvent?.Invoke(objectStuff);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //everything vision for anyone's use :D
        if (other != null)
        {
            if (other.GetComponent<LivingEntity>() != null)
            {
                //are they a Bee, use this:
                if (other.GetComponent<LivingEntity>().isBee == true)
                {
                    GameObject beeStuff = other.gameObject;
                    
                    memoryEvent?.Invoke(beeStuff);
                }
                
                //are they a Civ, use this:
                if (other.GetComponent<LivingEntity>().isBee == false)
                {
                    GameObject civStuff = other.gameObject;
                    
                    memoryEvent?.Invoke(civStuff);
                }
            }
            
            //is it food, use this:
            if (other.GetComponent<Food>() != null)
            {
                GameObject foodStuff = other.gameObject;
                
                memoryEvent?.Invoke(foodStuff);
            }
            
            //is it a dynamicObject, use this:
            if (other.GetComponent<DynamicObject>() != null)
            {
                GameObject objectStuff = other.gameObject;

                memoryEvent?.Invoke(objectStuff);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LivingEntity>() != null)
        {
            //removes Bees from current vision list
            if (other.GetComponent<LivingEntity>().isBee == true)
            {
                GameObject beeStuff = other.gameObject;
                
                beesInSight.Remove(beeStuff);
            }
            //removes Civs from current vision list
            if (other.GetComponent<LivingEntity>().isBee == false)
            {
                GameObject civStuff = other.gameObject;

                civsInSight.Remove(civStuff);
            }
        }
        //removes Food from current vision list
        if (other.GetComponent<Food>() != null)
        {
            GameObject honeyStuff = other.gameObject;
        
            foodInSight.Remove(honeyStuff);
        }
        //removes Objects from current vision list
        if (other.GetComponent<DynamicObject>() != null)
        {
            GameObject honeyStuff = other.gameObject;

            foodInSight.Remove(honeyStuff);
        }
    }
}
