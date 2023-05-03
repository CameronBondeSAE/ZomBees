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
    public int damage;
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
                collider.gameObject.GetComponent<Health>().Change(damage);
            }
        }
    }

    public void Inspect()
    {
        throw new System.NotImplementedException();
    }
    
    public void Consume()
    {
        throw new System.NotImplementedException();
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public string Description()
    {
        throw new System.NotImplementedException();
    }

    public void Pickup(GameObject _whoPickedMeUp)
    {
        whoPickedMeUp                         = _whoPickedMeUp;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled      = false;
    }
}
