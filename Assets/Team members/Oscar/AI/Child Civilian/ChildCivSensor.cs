using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Oscar
{
    public class ChildCivSensor : MonoBehaviour, ISense
    {
        public ChildCivController controller;
        
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
            hasRock = 7
        }
    }
}