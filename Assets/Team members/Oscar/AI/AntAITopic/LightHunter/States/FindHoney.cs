using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using Random = UnityEngine.Random;

public class FindHoney : OscarsLittleGuyMovement
{
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        BasicMovement(1);
        
        Wondering();
    }
}
