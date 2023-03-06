using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;

[Serializable]
public class WorldNode
{
    public Vector3 position;
    public bool isBlocked;

    public bool openNeighbour;

    public float cost;

    public WorldNode(Vector3 pos, bool blocked)
    {
        position = pos;
        isBlocked = blocked;
    }
}
