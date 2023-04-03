using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class OscarCivSensor : MonoBehaviour, ISense
{
    public OscarCivController civController;
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set(OscarCivilian.seeBee, civController.SeeBeeBool());
        aWorldState.Set(OscarCivilian.isScared, civController.IsScaredBool());
        aWorldState.Set(OscarCivilian.stayAlive, civController.StayAliveBool());
        aWorldState.Set(OscarCivilian.killBee, civController.KilledBeeBool());
        aWorldState.Set(OscarCivilian.isPlayerTalking, civController.PlayerIsTalking());
        aWorldState.Set(OscarCivilian.PrioritisePlayer, civController.PrioritiseThePlayer());
        
        aWorldState.EndUpdate();
    }
    public enum OscarCivilian
    {
        seeBee = 0,
        isScared = 1,
        stayAlive = 2,
        killBee = 3,
        isPlayerTalking = 4,
        PrioritisePlayer = 5
    }
}
