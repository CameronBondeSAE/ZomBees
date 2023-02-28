using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class LightSensorAI : MonoBehaviour, ISense
{
    public bool seeTheHoney;
    public bool hasTheHoney;
    public bool seeTheHive;
    public bool nearTheHive;
    public bool deliveredTheHoney;
    public bool seeTheLight;
    public bool enemyIsDead;

    public LightControllerAI lightController;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        //using bools as a temporary before changing to the control script returning values.
        aWorldState.BeginUpdate(aAgent.planner);
        
        aWorldState.Set(LightHunter.SeeHoney, seeTheHoney);
        aWorldState.Set(LightHunter.HasHoney, hasTheHoney);
        aWorldState.Set(LightHunter.SeeHive, seeTheHive);
        aWorldState.Set(LightHunter.NearHive, nearTheHive);
        aWorldState.Set(LightHunter.DeliveredHoney, deliveredTheHoney);
        aWorldState.Set(LightHunter.SeeLight, seeTheLight);
        aWorldState.Set(LightHunter.EnemyDead, enemyIsDead);
        
        aWorldState.EndUpdate();
    }
    
    public enum LightHunter
    {
        SeeHoney = 0,
        HasHoney = 1,
        SeeHive = 2,
        NearHive = 3,
        DeliveredHoney = 4,
        SeeLight = 5,
        EnemyDead = 6
    }
}
