using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Virginia;

public class CollectObjects : OscarsLittleGuyMovement
{
    private OscarVision vision;

    private Inventory inventory;

    private ChildCivController childControl;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        vision = aGameObject.GetComponentInChildren<OscarVision>();

        inventory = aGameObject.GetComponentInParent<Inventory>();
        
        childControl = aGameObject.GetComponent<ChildCivController>();
    }
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (inventory.heldItem == null)
        {
            if (vision.objectsInSight.Count > 0)
            {
                float distance = Vector3.Distance(transform.position, vision.objectsInSight[0].transform.position);

                if (distance < 2f)
                {
                    inventory.Pickup();
                }

                TurnTowards(vision.objectsInSight[0].transform.position);

                BasicMovement(10f);
            }
        }
        else if (inventory.heldItem != null)
        {
            childControl.DoIHaveObjects = true;
            Finish();
        }
    }
}