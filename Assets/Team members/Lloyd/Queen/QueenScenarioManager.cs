using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using UnityEngine;

public class QueenScenarioManager : MonoBehaviour, ISense
{
    public bool hasArrived;
    public bool hasResource;
    public bool investigating;
    public bool seeking;
    public bool spottedHuman;
    public bool attacking;

    public List<GameObject> followers = new List<GameObject>();

    public void AddFollower(GameObject follower)
    {
        followers.Add(follower);
    }

    public List<GameObject> harvestTargets = new List<GameObject>();

    public void AddHarvestTarget(GameObject target)
    {
        harvestTargets.Add(target);
    }

    public GameObject movePoint;

    public void AddMovePoint(GameObject newMovePoint)
    {
        movePoint = newMovePoint;
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
