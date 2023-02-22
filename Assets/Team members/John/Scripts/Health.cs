using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
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
        currHealth += changeAmount;

        if (currHealth <= 0)
        {
            //Die
            currHealth = 0;
        }
        Debug.Log("Taking damage");
        
    }
}

