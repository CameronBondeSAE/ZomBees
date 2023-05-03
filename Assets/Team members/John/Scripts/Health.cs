using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public delegate void DeathDelegate();
    public event DeathDelegate HealthReducedToZeroEvent;

    public delegate void HealthChangeDelegate(float changeAmount);

    public event HealthChangeDelegate HealthChangedEvent;


    public float maxHealth;
    private float minHealth = 0f;
    public float currHealth { get; private set; }
    private float amount;

    public void OnEnable()
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

