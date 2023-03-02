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
    public float feelerAmount;

    public float spacing;
    public float offset;

    public List<Transform> lightInSight;

    public List<Transform> honeyInSight;

    private void FixedUpdate()
    {
        for (int f = 0; f < feelerAmount; f++)
        {
            Vector3 direction = Quaternion.Euler(0f, f * spacing - offset, 0f) * guy.transform.forward;
            Physics.Raycast(guy.rb.transform.localPosition, direction, out RaycastHit hitInfo, distance, 255,
                QueryTriggerInteraction.Collide);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.GetComponentInParent<LightLength>() != null)
                {
                    Transform lightRay = hitInfo.transform;
                    
                    if (!lightInSight.Contains(lightRay)) 
                    { 
                        lightInSight.Add(hitInfo.transform); 
                    } 
                }

                if (hitInfo.collider.GetComponent<Honey>() != null)
                {
                    Transform honeyStuff = hitInfo.transform;

                    if (!honeyInSight.Contains(honeyStuff))
                    {
                        honeyInSight.Add(hitInfo.transform);
                    }
                }
                
            }
            else
            { 
                lightInSight.Clear();
                honeyInSight.Clear();
            }
        }
    }
}
