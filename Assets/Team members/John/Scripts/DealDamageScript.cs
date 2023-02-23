using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Time;

public class DealDamageScript : MonoBehaviour
{
    public float damage;
    public Health health;
    private IEnumerator coroutine;
    private bool isCoroutineRunning = false;

    private void Start()
    {
        coroutine = MyCoroutine();
    }

    public void Update()
    {
        if (health != null)
        {
            if (isCoroutineRunning != true)
            {
                isCoroutineRunning = true;
                StartCoroutine(coroutine);
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        health = collision.collider.GetComponent<Health>();
    }

    private void OnCollisionExit(Collision other)
    {
        health = null;
    }
    
    private IEnumerator MyCoroutine()
    {
        isCoroutineRunning = false;
        health.Change(-damage);
        yield return new WaitForSeconds(2);
        Debug.Log("2 seconds have passed");
        
    }
}

    
