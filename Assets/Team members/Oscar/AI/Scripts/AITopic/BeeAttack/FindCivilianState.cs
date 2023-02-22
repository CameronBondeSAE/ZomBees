using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class FindCivilianState : AntAIState
{
    public override void Enter()
    {
        base.Enter();
        Finish();
    }

    public override void Exit()
    {
        base.Exit();
        Finish();
    }
}
