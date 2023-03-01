using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class CollectHoney : AntAIState
{
    private LittleGuy littleGuy;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
                
        littleGuy = aGameObject.GetComponent<LittleGuy>();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
    }
}
