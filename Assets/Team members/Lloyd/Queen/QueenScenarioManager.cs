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

    public LookAtTarget lookAt;

    public Rigidbody rb;
    [ReadOnly] public GameObject queenParent;
    [ReadOnly] public CivStatComponent stats;

    public enum QueenStates
    {
        Idle,
        Patrol,
        Investigate,
        Attack,
        SpawnHive
    }

    public QueenStates currState;

    private void Awake()
    {
        StartQueen();
    }


    [Button]
    public void StartQueen()
    {
        rb = GetComponent<Rigidbody>();
        lookAt = GetComponent<LookAtTarget>();
        queenEvent = GetComponent<QueenEvent>();
        stats = GetComponent<CivStatComponent>();
        hearingComp = GetComponent<HearingComp>();
        
        StartBeeWings();
        
        initialised = true;
    }


    [Button]
    public void OnQueenChangeSwarmEvent()
    {
        queenEvent.OnChangeSwarmPoint(transform);
    }

    #region Attack

    public List<Transform> targetsList;

    #endregion

    #region Followers
    [Header("Followers")]
    
    public bool fullFollowers;

    public List<Follower> followers;

    public void AddFollower(Follower foll)
    {
        followers.Add(foll);
    }

    public int numSwarmers;

    #endregion

    #region Movement
    [Header("Movement")]
    
    public List<GameObject> patrolPoints;

    public List<GameObject> hiveSpots;

    public float flySpeed;
    
    #endregion

    #region BeeWings

    public BeeWingsManager beeWings;

    public int numWings;

    public float defaultFlapSpeed;
    public float defaultFlapAngle;

    private void StartBeeWings()
    {
        queenParent = new GameObject("ZOMBEE QUEEN PARENT") as GameObject;

        beeWings = GetComponentInChildren<BeeWingsManager>();

        for (int wings = numWings; wings > 0; wings--)
        {
            beeWings.AddWing();
        }

        beeWings.SpawnWings();

        beeWings.wingParent.transform.SetParent(queenParent.transform);
    }

    #endregion

    private QueenEvent queenEvent;

    public HearingComp hearingComp;

    public List<HearingComp.SoundData> heardSoundsList;

    public Vector3 queenVectorTarget;
    public Transform queenTransformTarget;

    #region ANTAI
    
    public bool dangerNearby;

    public bool hasArrived;
    
    public bool hasResource;
    
    public bool idle;
    
    public bool investigatingNoise;
    
    public bool moveToHiveSpot;
    public bool spawnHive;
    
    public bool seekingResource;

    public bool spottedResource;
    
    public bool initialised = false;
    
    public float resourceCount;
    
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

        aWorldState.Set("FullFollowers", fullFollowers);

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

            queenParent.transform.position = transform.position;
            queenParent.transform.rotation = transform.rotation;

        }
    }
}