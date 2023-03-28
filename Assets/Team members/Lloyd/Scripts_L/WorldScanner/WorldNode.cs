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
    public float nodeCost;

    public WorldNode(Vector3 pos, bool blocked, float cost)
    {
        position = pos;
        isBlocked = blocked;
        nodeCost = cost;
    }
    
    public Action<WorldNode, bool> ChangeNodeAction;
    public void OnChangeNodeState(WorldNode me, bool blocked)
    {
        ChangeNodeAction?.Invoke(me, blocked);
    }
}