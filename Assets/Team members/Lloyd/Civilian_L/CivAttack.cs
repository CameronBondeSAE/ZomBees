using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivAttack : AntAIState
{
    public List<GameObject> hitObjects = new List<GameObject>();

    public Vector3 punchBoxOffset;
    public Vector3 punchBoxSize;

    private Collider[] colliders = new Collider[10];

    public float punchDamage;

    public enum CivAttackType
    {
        Punch,
        Shoot,
        ThrowExplosive
    }

    public CivAttackType myAttack;

    public bool hasGun;

    public bool hasExplosive;

    public bool attacking = false;

    public override void Enter()
    {
        if (hasGun)
        {
            myAttack = CivAttackType.Shoot;
        }
        
        else if (hasExplosive)
        {
            myAttack = CivAttackType.ThrowExplosive;
        }

        else
        {
            myAttack = CivAttackType.Punch;
        }
        
        StartAttack(myAttack);
    }

    private void StartAttack(CivAttackType attackType)
    {
        if (attackType == CivAttackType.Punch)
        {
            StartCoroutine(Punch());
        }
        
        else if (attackType == CivAttackType.Shoot)
        {
            Shoot();
        }
        
        else if (attackType == CivAttackType.ThrowExplosive)
        {
            ThrowExplosive();
        }
    }
    
    [Button]
    private IEnumerator Punch()
    {
        hitObjects.Clear();

        Vector3 boxCenter = transform.position + punchBoxOffset;
        int numColliders = Physics.OverlapBoxNonAlloc(boxCenter, punchBoxSize * 0.5f, colliders);

        for (int i = 0; i < numColliders; i++)
        {
            Collider collider = colliders[i];

            if (collider.GetComponent<Health>() != null)
            {
                Health hitHealth = collider.GetComponent<Health>();
                hitHealth.Change(-punchDamage);
            }

            if (collider.GetComponent<IInteractable>() != null)
            {
                IInteractable interact = collider.GetComponent<IInteractable>();
                interact.Interact();
            }
        }

        yield return null;

        StopCoroutine(Punch());
    }

    private void Shoot()
    {
        
    }

    private void ThrowExplosive()
    {
        
    }
}
