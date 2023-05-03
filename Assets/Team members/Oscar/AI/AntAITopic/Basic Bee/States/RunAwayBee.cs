using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class RunAwayBee : OscarsLittleGuyMovement
{
    private float elapsedTime;
    
    public override void Enter()
    {
        base.Enter();
        NavmeshEnabled();
        Vector3 position = PatrolManager.singleton
            .hivePoints[Random.Range(0, PatrolManager.singleton.hivePoints.Count)].transform.position;
        NavmeshFindLocation(position);
        elapsedTime = 0f;
    }
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        elapsedTime += Time.deltaTime;
        if (elapsedTime <= 5f)
        {
            NavmeshToLocation();
        }
        else
        {
            Finish();
        }
        
        //Turn Away Functions and Basic Movement Functions
        //TurnAway(ears.loudestRecentSound.Source.transform.position);
        //BasicMovement(4f);
    }
    
    public override void Exit()
    {
        base.Exit();

        basicBeeControl.RunAway = false;
        NavMeshFinish();
    }
}
