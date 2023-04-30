using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Virginia;
using Random = UnityEngine.Random;

public class DeliverRocks : OscarsLittleGuyMovement
{
    //I want it to deliver it to the closest Civ or the players inventory.
    private Inventory inventory;

    private void OnEnable()
    {
        objectArrivedEvent += LocationArrivedAt;
    }

    public override void Create(GameObject aGameObject)
    {
    	base.Create(aGameObject);
    	inventory = aGameObject.GetComponentInParent<Inventory>();
    }

    public override void Enter()
    {
        base.Enter();
        NavmeshEnabled();
        Vector3 position = PatrolManager.singleton
            .pathsWithIndoors[Random.Range(0, PatrolManager.singleton.pathsWithIndoors.Count)].transform.position;
        NavmeshFindLocation(position);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (inventory.heldItem != null)
        {
            NavmeshToLocation();
        }
        else
        {
            Finish();
        }
    }

    private void LocationArrivedAt()
    {
        inventory.Dispose();
    }

    public override void Exit()
    {
        base.Exit();
        
        NavMeshFinish();
    }
}
