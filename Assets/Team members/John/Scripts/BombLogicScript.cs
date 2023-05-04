using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using Oscar;
using Sirenix.OdinInspector;
using UnityEngine;

public class BombLogicScript : DynamicObject, IItem, IInteractable
{
    [SerializeField]
    GameObject  whoPickedMeUp;
    public int damage = 100000;
    public SoundProperties soundProperties;
    Collider[] hitColliders;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, soundProperties.Radius);
    }

    [Button]
    public void Interact()
    {
        int numColliders;
        numColliders = Physics.OverlapSphereNonAlloc(gameObject.transform.position, soundProperties.Radius, 
            hitColliders, Int32.MaxValue, QueryTriggerInteraction.Ignore);
        
        for (int i = 0; i < numColliders; i++)
        {
            Collider collider = hitColliders[i];
            Health health = collider.GetComponent<Health>();
            if (collider != null && health != null)
            {
                collider.gameObject.GetComponent<Health>().Change(-damage);
            }
        }
    }

    public void Inspect()
    {
    }
    
    public void Consume()
    {
    }

    public void Dispose()
    {
    }

    public string Description()
    {
        return description;
    }

    public void Pickup(GameObject _whoPickedMeUp)
    {
        whoPickedMeUp                         = _whoPickedMeUp;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled      = false;
    }
}
