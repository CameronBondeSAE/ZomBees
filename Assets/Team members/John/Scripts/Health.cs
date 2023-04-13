using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public delegate void DeathDelegate();
    public static event DeathDelegate HealthReducedToZeroEvent;

    public delegate void HealthChangeDelegate(float changeAmount);

    public static event HealthChangeDelegate HealthChangedEvent;


    public float maxHealth;
    private float minHealth = 0;
    [SerializeField]private float currHealth;
    private float amount;

    private void Start()
    {
        currHealth = maxHealth;
    }

    public void Change(float changeAmount)
    {
        if (HealthChangedEvent != null)
        {
            HealthChangedEvent(changeAmount);
        }
        currHealth += changeAmount;

        if (currHealth >= maxHealth)
        {
            currHealth = maxHealth;
        }

        if (currHealth <= 0)
        {
            if (HealthReducedToZeroEvent != null)
            {
                HealthReducedToZeroEvent();
            }
            currHealth = 0;
        }
        //Debug.Log("Taking damage");
        
    }
}

