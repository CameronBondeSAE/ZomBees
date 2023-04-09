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
    #region BeeWings

    public GameObject beeWings;
    public int numWings;
    
    public float defaultFlapSpeed;

    private void StartBeeWings()
    {
        GameObject instantiatedPrefab = Instantiate(beeWings);
        BeeWingsManager beeWingManager = instantiatedPrefab.GetComponent<BeeWingsManager>();

        //customise later
        beeWingManager.xDistance = 0.3f;
        beeWingManager.zDistance = 0.3f;

        for (int wings = numWings; wings > 0; wings--)
        {
            beeWingManager.AddWing();
        }
        beeWingManager.SpawnWings();
        
        beeWingManager.OnChangeStatEvent(-90, defaultFlapSpeed, true);
        
        beeWingManager.wingParent.transform.SetParent(transform);
    }
    
    #endregion
    
    
    private QueenEvent queenEvent;

    private CivStatComponent stats;

    public List<GameObject> patrolPoints;

    public HearingComp hearingComp;

    public List<Transform> hiveSpots;

    public List<HearingComp.SoundData> heardSoundsList;

    public List<Transform> targetsList;

    public enum QueenStates
    {
        MoveToPoint,
        Patrol,
        Investigate,
        Attack,
        SpawnHive,
        Move
    }

    public QueenStates currState;
    private QueenStates decidedOnThisState;

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

        aWorldState.EndUpdate();
    }

    #endregion

    [Button]
    private void Awake()
    {
        queenEvent = GetComponent<QueenEvent>();
        stats = GetComponent<CivStatComponent>();
        hearingComp = GetComponent<HearingComp>();

        initialised = true;
        
       // StartBeeWings();
    }

    public List<HearingComp.SoundData> GetSounds()
    {
        return hearingComp.soundsList;
    }

    public void Update()
    {
        if (initialised)
        {
            heardSoundsList = GetSounds();
            resourceCount = stats.beenessDisplayed;

            Decide();
        }
    }

    public void Decide()
    {/*
        if (isMoving)
            decidedOnThisState = QueenStates.MoveToPoint;*/
        //return;
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