using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Virginia;

public class CollectFood : OscarsLittleGuyMovement
{
    private OscarVision vision;
    private Inventory inventory;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        vision = aGameObject.GetComponentInChildren<OscarVision>();

        inventory = aGameObject.GetComponentInParent<Inventory>();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        TurnTowards(vision.foodInSight[0].transform.position);
        BasicMovement(4f);
        
        if (inventory.heldItem == null)
        {        
            inventory.Pickup();
        }
        
        if (inventory.heldItem != null)
        {
            if (inventory.heldItem.Description() == "Food")
            {
                Finish();
            }
            else
            {
                inventory.Dispose();
            }
        }
    }
}
