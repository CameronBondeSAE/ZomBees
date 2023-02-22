using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageScript : MonoBehaviour
{
    public float damage;

    public void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Health health = collision.collider.GetComponent<Health>();

        if (health != null)
        {
            health.Change(-damage);
        }
    }
}

    
