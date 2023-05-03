using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using UnityEngine;

namespace Oscar
{
    public class BasicBeeSensor : MonoBehaviour, ISense
    {
        public OscarControllerAI controllerAI;
        
        public bool deliveredTheFood;
        public bool beeStayAlive;

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            
            aWorldState.Set(BasicBee.SeeFood, controllerAI.seeTheFood());
            aWorldState.Set(BasicBee.HasFood, controllerAI.hasTheFood());
            aWorldState.Set(BasicBee.DeliveredFood, controllerAI.DeliverTheFood());
            aWorldState.Set(BasicBee.StayAlive, beeStayAlive);
            aWorldState.Set(BasicBee.SeeCivilian, controllerAI.seeCivilians());
            aWorldState.Set(BasicBee.EnemyDead, controllerAI.enemyIsDead());
            aWorldState.Set(BasicBee.CanHearCreatureRepellant, controllerAI.RunAway);
            
            aWorldState.EndUpdate();
        }
    }
    
    public enum BasicBee
    {
        SeeFood = 0,
        HasFood = 1,
        DeliveredFood = 2,
        StayAlive = 3,
        SeeCivilian = 4,
        EnemyDead = 5,
        CanHearCreatureRepellant = 6
    }
}