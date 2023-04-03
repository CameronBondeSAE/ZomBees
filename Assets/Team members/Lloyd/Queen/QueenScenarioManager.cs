using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;
using Lloyd;

public class QueenScenarioManager : MonoBehaviour, ISense
{
    private QueenEvent queenEvent;

    private QueenLerpTowards lerpTowards;

    private bool isAlive = true;

    public enum QueenState
    {
        SeekHuman,
        Investigate,
        Attack,
        SeekHive,
        SpawnHive,
        
        Attacking
    }

    public QueenState myState;

    public bool isMoving=false;

    public bool hasArrived=false;

    public bool hasResource=false;
    public bool investigating=false;
    public bool seeking=false;
    public bool spottedHuman=false;
    
    public bool attacking=false;

    private LookTowards lookTowards;

    public List<GameObject> followers = new List<GameObject>();

    public List<GameObject> harvestTargets = new List<GameObject>();

    public GameObject movePoint;

    public GameObject lookPoint;

    public float thinkTime;

    public Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        queenEvent = GetComponent<QueenEvent>();

        StartCoroutine(Decide());
    }

    public void FixedUpdate()
    {
        if(rb!=null)
        queenEvent.OnChangeSwarmPoint(rb.transform);
    }

    private IEnumerator Decide()
    {
        //check for scent, heard sound, etc
        yield return new WaitForSeconds(thinkTime);

        if (hasResource)
        {
            myState = QueenState.SeekHive;
        }

        if (spottedHuman)
        {
            if (harvestTargets.Count > 0)
            {

            }
        }
    }

    public void AddFollower(GameObject follower)
    {
        followers.Add(follower);
    }

    public void SetLookPoint(GameObject newLookPoint)
    {
        lookPoint = newLookPoint;
        //lookTowards.SetTarget(lookPoint.transform);
    }
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set("Arrived", hasArrived);

        aWorldState.Set("HasResource", hasResource);

        aWorldState.Set("Investigating", investigating);

        aWorldState.Set("Seeking", seeking);

        aWorldState.Set("SpottedHuman", spottedHuman);

        aWorldState.Set("Attacking", attacking);

        aWorldState.EndUpdate();
    }
}