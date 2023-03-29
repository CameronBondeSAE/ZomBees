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
        aWorldState.Set(OscarCivilian.nearBee, civController.NearBeeBool());
        aWorldState.Set(OscarCivilian.isScared, civController.IsScaredBool());
        aWorldState.Set(OscarCivilian.stayAlive, civController.StayAliveBool());
        aWorldState.Set(OscarCivilian.killBee, civController.KilledBeeBool());
    }
    public enum OscarCivilian
    {
        seeBee = 0,
        nearBee = 1,
        isScared = 2,
        stayAlive = 3,
        killBee = 4
    }
}
