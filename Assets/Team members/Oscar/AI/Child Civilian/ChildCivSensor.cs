using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Oscar
{
    public class ChildCivSensor : MonoBehaviour, ISense
    {
        public ChildCivController controller;

        public AntAIAgent aiAgent;
        
        private void Awake()
        {
            aiAgent = GetComponent<AntAIAgent>();
        }

        public void DefaultState()
        {
            //set goal to the default one.
        }
        public void CollectState()
        {
            //set goal to the collection one.
        }

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);

            aWorldState.Set(CivilianChild.Idle, controller.AmIIdle());
            aWorldState.Set(CivilianChild.stayAlive, controller.AmIAlive());
            aWorldState.Set(CivilianChild.isFollowing, controller.AmIFollowing());
            aWorldState.Set(CivilianChild.isConversing, controller.AmIConversing());
            aWorldState.Set(CivilianChild.deliveredRocks, controller.DeliverTheRocks());
            aWorldState.Set(CivilianChild.isScared, controller.AmIScared());
            aWorldState.Set(CivilianChild.seeRock, controller.CanISeeRocks());
            aWorldState.Set(CivilianChild.hasRock, controller.DoIHaveRocks());
            aWorldState.Set(CivilianChild.getStuff, controller.GetTheStuff());
            aWorldState.Set(CivilianChild.hasStuff, controller.DoIHaveStuff());
            aWorldState.Set(CivilianChild.returnedStuff, controller.StuffDelivered());
            aWorldState.Set(CivilianChild.isHungry, controller.ImHungry());
            aWorldState.Set(CivilianChild.hasFood, controller.DoIHaveFood());
            aWorldState.Set(CivilianChild.seeFood, controller.ISeeFood());
            aWorldState.Set(CivilianChild.Hide, controller.ShouldIHide());
            
            aWorldState.EndUpdate();
        }

        public enum CivilianChild
        {
            Idle = 0,
            stayAlive = 1,
            isFollowing = 2,
            isConversing = 3,
            deliveredRocks = 4,
            isScared = 5,
            seeRock = 6,
            hasRock = 7,
            getStuff = 8,
            hasStuff = 9,
            returnedStuff = 10,
            isHungry = 11,
            hasFood = 12,
            seeFood = 13,
            Hide = 14
        }
    }
}