using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class OscarSensorAI : MonoBehaviour, ISense
{
    public OscarControllerAI oscarController;
    

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        
        aWorldState.Set(LightHunter.SeeHoney, oscarController.seeTheFood());
        aWorldState.Set(LightHunter.HasHoney, oscarController.hasTheFood());
        aWorldState.Set(LightHunter.SeeLight, oscarController.seeTheLight());
        aWorldState.Set(LightHunter.EnemyDead, oscarController.enemyIsDead());
        
        aWorldState.EndUpdate();
    }
    
    public enum LightHunter
    {
        SeeHoney = 0,
        HasHoney = 1,
        SeeLight = 2,
        EnemyDead = 3
    }
}
