using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenEventArgs : EventArgs
{
    public QueenPathfinding pathfinding;

    public QueenLookTowardsState lookTowards;

    public QueenAttack queenAttack;

    public bool activated;
}
