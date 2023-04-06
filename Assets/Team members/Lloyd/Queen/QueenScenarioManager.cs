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

    public bool arrived = false;

    private LookAtTarget lookAtTarget;

    public List<GameObject> followers = new List<GameObject>();

    public List<GameObject> harvestTargets = new List<GameObject>();

    public Vector3 movePoint;

    public Vector3 lookPoint;

    public Rigidbody rb;

    #region ANTAI

    public bool hasResource = false;

    public bool isMoving = false;

    public bool hasArrived = false;

    public bool investigatingNoise = false;

    public bool seekingResource = true;

    public bool spottedResource = false;

    public bool moveToHiveSpot = false;

    public bool spawnHive = false;

    public bool dangerNearby = false;

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

    private void StartQueen()
    {
        rb = GetComponent<Rigidbody>();
        queenEvent = GetComponent<QueenEvent>();
    }


    public void AddFollower(GameObject follower)
    {
        followers.Add(follower);
    }
}