using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public delegate void Death();
    public static event Death HealthReducedToZero;

    public delegate void HealthChange();

    public static event HealthChange HealthChanged;


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
        if (HealthChanged != null)
        {
            HealthChanged();
        }
        currHealth += changeAmount;

        if (currHealth <= 0)
        {
            if (HealthReducedToZero != null)
            {
                HealthReducedToZero();
            }
            currHealth = 0;
        }
        //Debug.Log("Taking damage");
        
    }
}

