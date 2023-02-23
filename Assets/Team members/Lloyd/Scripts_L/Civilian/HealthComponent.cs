using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int maxHP;
    public int HP;

    public bool isAlive = true;

    public void ChangeHP(int amount)
    {
        HP += amount;

        if (HP <= 0)
        {
            HP = 0;
            Death();
        }

        if (HP >= maxHP)
            HP = maxHP;
    }

    private void Death()
    {
        //death event
        isAlive = false;
    }
}
