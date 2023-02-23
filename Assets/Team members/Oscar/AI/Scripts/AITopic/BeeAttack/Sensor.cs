using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using MyNamespace;
using UnityEngine;

public class Sensor : MonoBehaviour, ISense
{
    public bool civilianFound;
    public bool nearCivilian;
    public bool civilianDead;
    public bool seeHive;
    public bool nearHive;
    public bool killReported;

    public Control control;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        aWorldState.Set(ScenarioAI.CivilianFound, control.CanSeeCivilGuy());
        aWorldState.Set(ScenarioAI.NearCivilian, nearCivilian);
        aWorldState.Set(ScenarioAI.CivilianDead, civilianDead);
        aWorldState.Set(ScenarioAI.SeeHive, seeHive);
        aWorldState.Set(ScenarioAI.NearHive, nearHive);
        aWorldState.Set(ScenarioAI.KillReported, killReported);
        
        aWorldState.EndUpdate();
    }
    
    public enum ScenarioAI
    {
        CivilianFound = 0,
        NearCivilian = 1,
        CivilianDead = 2,
        SeeHive = 3,
        NearHive = 4,
        KillReported = 5
    }
    
}

