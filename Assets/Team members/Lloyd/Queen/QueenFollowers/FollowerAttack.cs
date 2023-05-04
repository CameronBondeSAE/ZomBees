using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using Oscar;
using UnityEngine;

public class FollowerAttack : MonoBehaviour
{
    public float followerAttackAmount;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DynamicObject>() != null)
        {
            DynamicObject obj = other.GetComponent<DynamicObject>();
            if(obj.isCiv){
                Health newHealth = obj.GetComponent<Health>();
                newHealth.Change(-followerAttackAmount);
            }
        }
    }
}
