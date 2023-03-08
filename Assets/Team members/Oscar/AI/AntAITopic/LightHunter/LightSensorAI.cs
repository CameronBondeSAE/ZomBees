using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class LightSensorAI : MonoBehaviour, ISense
{
    public LightControllerAI lightController;
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        //using bools as a temporary before changing to the control script returning values.
        aWorldState.BeginUpdate(aAgent.planner);
        
        aWorldState.Set(LightHunter.SeeHoney, lightController.seeTheHoney());
        aWorldState.Set(LightHunter.HasHoney, lightController.hasTheHoney());
        aWorldState.Set(LightHunter.SeeLight, lightController.seeTheLight());
        aWorldState.Set(LightHunter.EnemyDead, lightController.enemyIsDead());
        
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
