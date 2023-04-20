using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Oscar;
using Random = UnityEngine.Random;

public class FindFood : OscarsLittleGuyMovement
{
    private LittleGuy littleGuy;

    public override void Enter()
    {
        base.Enter();

        //GetComponentInChildren<ColourChangeShader>().attackPhase = false;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        BasicMovement(1f);
        Wondering();
    }
}
