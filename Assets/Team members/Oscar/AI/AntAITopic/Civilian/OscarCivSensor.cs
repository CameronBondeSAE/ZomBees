using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using UnityEngine.Serialization;

public class OscarCivSensor : MonoBehaviour, ISense
{
    public OscarBruteCivController bruteCivController;
    
    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);

        aWorldState.Set(OscarCivilian.seeBee, bruteCivController.SeeBeeBool());
        aWorldState.Set(OscarCivilian.isScared, bruteCivController.IsScaredBool());
        aWorldState.Set(OscarCivilian.stayAlive, bruteCivController.StayAliveBool());
        aWorldState.Set(OscarCivilian.killBee, bruteCivController.KilledBeeBool());
        aWorldState.Set(OscarCivilian.isPlayerTalking, bruteCivController.PlayerIsTalking());
        aWorldState.Set(OscarCivilian.PrioritisePlayer, bruteCivController.PrioritiseThePlayer());
        
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
