using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;

public class QueenEventArgs : EventArgs
{
    public QueenLerpTowards lerpTowards;

    public LookTowards lookTowards;

    public QueenAttack queenAttack;

    public bool activated;
}
