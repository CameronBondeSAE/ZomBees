using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class QueenThink : AntAIState
{
    private CivStatComponent stats;

    public float resources;

    public override void Enter()
    {
        stats = GetComponentInParent<CivStatComponent>();

        resources = stats.Beeness;
        
        
    }
}
