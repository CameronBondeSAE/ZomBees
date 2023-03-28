using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianProfile : MonoBehaviour
{
   public enum EmotionalEvents
   {
      HeardSomething,
      SawSomething,
      PlayerOrdered,
      TurnedIntoBee,
      TurnedBackFromBee
   };
   
   public QuestScriptable currentQuest;
   public enum CivGoal
   {
      HideInSafeRoom,
      FollowPlayer,
      FollowPlayerOrder,
      Interact,
      LookingForFood,
      TurningIntoBee
   }
   public CivGoal currentGoal;

   public void ChangeState(CivGoal newGoal)
   {
      if (currentGoal == newGoal)
         return;

      currentGoal = newGoal;
   }
}
