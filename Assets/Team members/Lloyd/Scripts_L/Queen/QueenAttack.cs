using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAttack : MonoBehaviour
{
    // "sight"
    public float radius;

    public QueenEvent queenEvent;

    private SphereCollider sphereCollider;

    private Transform me;

    public bool attacking;

    private GameObject playerObj;

    private void Start()
    {
        me = transform;
        queenEvent = GetComponent<QueenEvent>();
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = false;
        sphereCollider.radius = radius;
    }

    private void FixedUpdate()
    {
        if(!attacking)
        queenEvent.OnChangeSwarmPoint(me);
        
        else if (attacking && playerObj!=null)
        {
            queenEvent.OnChangeSwarmPoint(playerObj.transform);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        playerObj = collision.gameObject;
        IPlayer player = playerObj.GetComponent<IPlayer>();
        if (player != null)
        {
            attacking = true;
            //Debug.Log("Player detected!");
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(collision.transform.position, transform.position);

            Debug.DrawRay(transform.position, direction * distance, Color.red);
        }

        queenEvent.OnChangeSwarmPoint(playerObj.transform);
    }

    private void OnTriggerExit(Collider player)
    {
        //Debug.Log("Player lost");
        queenEvent.OnChangeSwarmPoint(me);
        attacking = false;
    }
}