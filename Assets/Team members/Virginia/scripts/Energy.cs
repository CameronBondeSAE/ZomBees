using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private float Amount;
    public float MaxHealth = 50;


    void FixedUpadate()
    {
        Amount = Amount * Time.deltaTime;
    }  

    public void Change(float changeAmount)
    {
        MaxHealth += changeAmount;

        if (MaxHealth <= 0)
        {
            
        }
        Debug.Log("dead");
    }

}