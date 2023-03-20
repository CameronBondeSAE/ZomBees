using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Oscar
{
    public class BasicBeeSensor : MonoBehaviour, ISense
    {
        public OscarControllerAI controllerAI;
        
        public bool seeTheHive;
        public bool nearTheHive;
        public bool deliveredTheFood;
        public bool beeStayAlive;

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            
            aWorldState.Set(BasicBee.SeeFood, controllerAI.seeTheFood());
            aWorldState.Set(BasicBee.HasFood, controllerAI.hasTheFood());
            aWorldState.Set(BasicBee.SeeHive, seeTheHive);
            aWorldState.Set(BasicBee.NearHive, nearTheHive);
            aWorldState.Set(BasicBee.DeliveredFood, deliveredTheFood);
            aWorldState.Set(BasicBee.StayAlive, beeStayAlive);
            aWorldState.Set(BasicBee.SeeCivilian, controllerAI.seeCivilians());
            aWorldState.Set(BasicBee.EnemyDead, controllerAI.enemyIsDead());
            
            aWorldState.EndUpdate();
        }
    }
    public enum BasicBee
    {
        SeeFood = 0,
        HasFood = 1,
        SeeHive = 2,
        NearHive = 3,
        DeliveredFood = 4,
        StayAlive = 5,
        SeeCivilian = 6,
        EnemyDead = 7
    }
}