using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;
using Lloyd;
using Sirenix.OdinInspector;

public class QueenScenarioManager : MonoBehaviour, ISense
{
    //big daddy queen brain

    [ReadOnly]
    public Rigidbody rb;
    [ReadOnly]
    public GameObject queenParent;
    [ReadOnly]
    public CivStatComponent stats;
    public enum QueenStates
    {
        Idle,
        MoveToPoint,
        Patrol,
        Investigate,
        Attack,
        SpawnHive,
        Move
    }
    public QueenStates currState;
    private QueenStates decidedOnThisState;
    
    [Button]
    public void StartQueen()
    {
        rb = GetComponent<Rigidbody>();
        queenEvent = GetComponent<QueenEvent>();
        stats = GetComponent<CivStatComponent>();
        hearingComp = GetComponent<HearingComp>();
        
        bob = GetComponent<PerlinBob>();
        bob.enabled = false;
        

        StartBeeWings();
        
        ChangeQueenState(QueenStates.Idle);
        
        initialised = true;
    }

    #region Attack

    public List<Transform> targetsList;

    #endregion
    
    #region TraversalTargetPoints
    
    public List<GameObject> patrolPoints;
    
    
    
    public List<Transform> hiveSpots;
    #endregion

    #region Idle
    
    public bool idle=false;
    
    public PerlinBob bob;

    public float minIdleTime=5f;
    public float maxIdleTime=30f;

    public void FlipIdle()
    {
        idle = !idle;
    }

    #endregion
    
    #region BeeWings

    public GameObject anchorPos;
    public GameObject beeWings;
    public int numWings;
    
    public float defaultFlapSpeed;

    private void StartBeeWings()
    {   
        queenParent = new GameObject("ZOMBEE QUEEN PARENT") as GameObject;

        anchorPos.transform.rotation = new Quaternion(0, -90, 0, 1);
        
        GameObject instantiatedPrefab = Instantiate(beeWings);
        BeeWingsManager beeWingManager = instantiatedPrefab.GetComponent<BeeWingsManager>();
        beeWingManager.anchorPos = anchorPos;

        //customise later
        beeWingManager.xDistance = 0.3f;
        beeWingManager.zDistance = 0.3f;

        for (int wings = numWings; wings > 0; wings--)
        {
            beeWingManager.AddWing();
        }
        beeWingManager.SpawnWings();
        
        beeWingManager.OnChangeStatEvent(-90, defaultFlapSpeed, true);
        
        beeWingManager.wingParent.transform.SetParent(queenParent.transform);
    }
    
    #endregion
    
    private QueenEvent queenEvent;
    
    public HearingComp hearingComp;
    
    public List<HearingComp.SoundData> heardSoundsList;

    public Vector3 queenVectorTarget;
    public Transform queenTransformTarget;

    #region ANTAI

    public float resourceCount;

    public bool hasResource = false;

    public bool isMoving = false;

    public bool hasArrived = false;

    public bool investigatingNoise = false;

    public bool seekingResource = true;

    public bool spottedResource = false;

    public bool moveToHiveSpot = false;

    public bool spawnHive = false;

    public bool dangerNearby = false;

    public bool initialised = false;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set("Arrived", hasArrived);

        aWorldState.Set("HasResource", hasResource);

        aWorldState.Set("InvestigatingNoise", investigatingNoise);

        aWorldState.Set("SeekingResource", seekingResource);
        
        aWorldState.Set("SpottedResource", spottedResource);

        aWorldState.Set("MoveToHive", moveToHiveSpot);

        aWorldState.Set("SpawnHive", spawnHive);

        aWorldState.Set("DangerNearby", dangerNearby);

        aWorldState.Set("Idle", idle);

        aWorldState.EndUpdate();
    }

    #endregion

    public List<HearingComp.SoundData> GetSounds()
    {
        return hearingComp.soundsList;
    }

    public void Execute(float adeltatime, float timescale)
    {
        if (initialised)
        {
            heardSoundsList = GetSounds();
            resourceCount = stats.beenessDisplayed;

            if (currState == QueenStates.Idle)
            {
                bob.enabled = true;
                return;
            }
            else
            {
                bob.enabled = false;
            }
            
            Decide();
        }
    }

    public void Decide()
    {/*
        if (isMoving)
            decidedOnThisState = QueenStates.MoveToPoint;*/
        //return;
        if (idle)
        {
            decidedOnThisState = QueenStates.Idle;
        }
        else
        {
            decidedOnThisState = QueenStates.Patrol;

            if (resourceCount < 1)
            {
                if (!isMoving)
                {
                    seekingResource = true;

                    if (targetsList.Count > 0)
                    {
                        dangerNearby = true;
                        spottedResource = true;
                        decidedOnThisState = QueenStates.Attack;
                    }
                    else if (heardSoundsList.Count > 0)
                    {
                        investigatingNoise = true;
                        decidedOnThisState = QueenStates.Investigate;
                    }
                }
            }

            else
            {
                seekingResource = false;
                hasResource = true;
                decidedOnThisState = QueenStates.SpawnHive;
            }
        }

        ChangeQueenState(decidedOnThisState);
    }

    public void ChangeQueenState(QueenStates newState)
    {
        if (newState != currState)
            currState = newState;
    }


    public void AddFollower(GameObject follower)
    {
        //followers.Add(follower);
    }
}