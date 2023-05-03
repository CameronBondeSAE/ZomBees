using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;

public class FollowerAttack : MonoBehaviour
{
    public float followerAttackAmount;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICivilian>() != null)
        {
            if (other.GetComponent<Health>() != null)
            {
                Health newHealth = other.GetComponent<Health>();
                newHealth.Change(-followerAttackAmount);
            }
        }
    }
}
