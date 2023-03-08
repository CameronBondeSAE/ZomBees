using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using UnityEngine.Serialization;

public class LightVisionAI : MonoBehaviour
{
    public LittleGuy guy;
    public float distance;
    //public int feelerAmount;

    public List<GameObject> lightInSight;

    public List<GameObject> honeyInSight;

    private void FixedUpdate()
    {
        //for (int f = 0; f < feelerAmount; f++)
        {
            Vector3 direction = Quaternion.Euler(0f, 0f, 0f) * guy.transform.forward;
            Physics.Raycast(guy.rb.transform.localPosition, direction, out RaycastHit hitInfo, distance, 255,
                QueryTriggerInteraction.Collide);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.GetComponentInParent<LightLength>() != null)
                {
                    GameObject lightRay = hitInfo.collider.gameObject;
                    
                    if (!lightInSight.Contains(lightRay)) 
                    { 
                        lightInSight.Add(lightRay); 
                    } 
                }

                if (hitInfo.collider.GetComponent<Honey>() != null)
                {
                    GameObject honeyStuff = hitInfo.collider.gameObject;

                    if (!honeyInSight.Contains(honeyStuff))
                    {
                        honeyInSight.Add(honeyStuff);
                    }
                }
                
                //avoid walls coming to a script near you!
                
            }
            else
            { 
                lightInSight.Clear();
                honeyInSight.Clear();
            }
        }
    }
}
