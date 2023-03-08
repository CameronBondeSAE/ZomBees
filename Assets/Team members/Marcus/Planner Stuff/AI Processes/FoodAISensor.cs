using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Marcus
{
    public class FoodAISensor : MonoBehaviour, ISense
    {
        public FoodAIController controller;
        

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);

            aWorldState.Set(CivilianPlannerTest.IsHungry, controller.IsHungry());
            aWorldState.Set(CivilianPlannerTest.CanSeeFood, controller.CanSeeFood());
            aWorldState.Set(CivilianPlannerTest.HasFood, controller.HasFood());
            
            aWorldState.EndUpdate();
        }
        
        public enum CivilianPlannerTest
        {
            IsHungry = 0,
            CanSeeFood = 1,
            HasFood = 2
        }
    }
}
