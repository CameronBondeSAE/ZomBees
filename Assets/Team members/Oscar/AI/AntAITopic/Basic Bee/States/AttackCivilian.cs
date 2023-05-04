using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Virginia;

public class AttackCivilian : OscarsLittleGuyMovement
{
    private OscarVision vision;
    private Inventory inventory;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        vision = aGameObject.GetComponentInChildren<OscarVision>();
        inventory = aGameObject.GetComponentInParent<Inventory>();
    }

    public override void Enter()
    {
        base.Enter();
        
        //GetComponentInChildren<ColourChangeShader>().attackPhase = true;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        if (vision.civsInSight.Count >= 1)
        {
            float distance = Vector3.Distance(littleGuy.transform.position, vision.foodInSight[0].transform.position);

            if (distance < 2f)
            {
                Collider[] colliders = Physics.OverlapSphere(inventory.hand.position,2f,255);
                
                for (int i = 0; i < colliders.Length; i++)
                {
                    // Check if the collider belongs to the player
                    Health enemyHealth = colliders[i].GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        // Deal damage to the player
                        if (enemyHealth != null && enemyHealth.currHealth > 0)
                            enemyHealth.Change(-1000000000000000f);
                    }
                    
                }
            }
            
            TurnTowards(vision.civsInSight[0].transform.position);
            
            BasicMovement(2f);
        }
        else
        {
            Finish();
        }
    }
}
