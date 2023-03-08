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
    public void ChangeNode(Vector3 pos, bool blocked, float cost)
    {
        position = pos;
        isBlocked = blocked;
        nodeCost = cost;
    }
    
    public Action<WorldNode, Vector3, bool, float> ChangeNodeState;
    public void OnChangeNodeState(WorldNode me, Vector3 pos, bool blocked, float cost)
    {
        ChangeNodeState?.Invoke(me,pos, blocked, cost);
    }
}