using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class LightLength : DynamicObject
{
    public float initialLength;
    public float distance;
    public GameObject lightCone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DynamicObject>() != null)
        {
            other.GetComponent<DynamicObject>().isLit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DynamicObject>() != null)
        {
            other.GetComponent<DynamicObject>().isLit = false;
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, 255, QueryTriggerInteraction.Ignore))
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            //float adjustedLength = distance;
            lightCone.transform.localScale = new Vector3(7f, 7f, distance);
        }
        else
        {
            lightCone.transform.localScale = new Vector3(7f, 7f, initialLength);
        }
    }
}
