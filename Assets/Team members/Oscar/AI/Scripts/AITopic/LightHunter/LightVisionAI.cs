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

    private void FixedUpdate()
    {
        List<Transform> stillInSight = new List<Transform>();

        for (int felt = 0; felt < feelerAmount; felt++)
        {
            Vector3 direction = Quaternion.Euler(0f, felt * spacing - offset, 0f) * guy.transform.forward;
            Physics.Raycast(guy.rb.transform.localPosition, direction, out RaycastHit hitInfo, distance, 255,
                QueryTriggerInteraction.Collide);
            if (hitInfo.collider.GetComponentInParent<LightLength>() != null)
            {
                Transform lightRay = hitInfo.transform;
                    
                if (!lightInSight.Contains(lightRay))
                { 
                    lightInSight.Add(hitInfo.transform);
                }
            }
        }
    }
}
