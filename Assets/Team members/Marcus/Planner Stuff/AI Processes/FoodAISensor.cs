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
            aWorldState.Set(CivilianPlannerTest.HasItem, controller.HasItem());
            aWorldState.Set(CivilianPlannerTest.CanSeeBees, controller.CanSeeBee());
            aWorldState.Set(CivilianPlannerTest.IsDaytime, controller.Day());

            aWorldState.EndUpdate();
        }
        
        public enum CivilianPlannerTest
        {
            IsHungry = 0,
            CanSeeFood = 1,
            HasFood = 2,
            IsScared = 3,
            CanSeeBees = 4,
            IsDaytime = 5,
            IsHidden = 6,
            CanHearBees = 7,
            HasItem = 8,
            TorchOn = 9
        }
    }
}
