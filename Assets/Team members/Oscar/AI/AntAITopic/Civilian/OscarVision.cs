using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class OscarVision : MonoBehaviour
{
    #region Variables
    
    public List<GameObject> beesInSight;

    public List<GameObject> foodInSight;

    public List<GameObject> civsInSight;

    public List<GameObject> objectsInSight;

    public List<GameObject> lightInSight;
    
    public delegate void OnObjectSeen(GameObject thing);
    public event OnObjectSeen memoryEvent;
    
    #endregion

    #region OnTriggerEnter
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
            if (other.GetComponent<DynamicObject>() != null)
            {
                //is it food, use this:
                if (other.GetComponent<DynamicObject>().isFood == true)
                {
                    GameObject foodStuff = other.gameObject;

                    if (!foodInSight.Contains(foodStuff))
                    {
                        foodInSight.Add(foodStuff);

                        memoryEvent?.Invoke(foodStuff);
                    }
                }
                //if its an Object (not Food), use this:
                if (other.GetComponent<DynamicObject>().isFood == false)
                {
                    GameObject objectStuff = other.gameObject;
                    
                    if (!objectsInSight.Contains(objectStuff))
                    {
                        objectsInSight.Add(objectStuff);
                        
                        memoryEvent?.Invoke(objectStuff);
                    }
                }
                //is lit up, use this:
                if (other.GetComponent<DynamicObject>().isLit == true)
                {
                    GameObject litObj = other.gameObject;
                    
                    if (!lightInSight.Contains(litObj)) 
                    { 
                        lightInSight.Add(litObj); 
                    }
                }
            }
        }
    }
    #endregion

    #region OnTriggerStay
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

            if (other.GetComponent<DynamicObject>() != null)
            {
                //is it a food, use this:
                if (other.GetComponent<DynamicObject>().isFood == true)
                {
                    GameObject foodStuff = other.gameObject;

                    memoryEvent?.Invoke(foodStuff);
                }
                //is it a Object, use this:
                if (other.GetComponent<DynamicObject>().isFood == false)
                {
                    GameObject objectStuff = other.gameObject;

                    memoryEvent?.Invoke(objectStuff);
                }
            }
        }
    }
    #endregion

    #region OnTriggerExit
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
        if (other.GetComponent<DynamicObject>() != null)
        {
            //removes Food from current vision list
            if (other.GetComponent<DynamicObject>().isFood == true)
            {
                GameObject honeyStuff = other.gameObject;
                        
                foodInSight.Remove(honeyStuff);
            }
            //removes Objects from current vision list
            if (other.GetComponent<DynamicObject>().isFood == false)
            {
                GameObject honeyStuff = other.gameObject;

                foodInSight.Remove(honeyStuff);
            }
        }
    }    
    #endregion
}
