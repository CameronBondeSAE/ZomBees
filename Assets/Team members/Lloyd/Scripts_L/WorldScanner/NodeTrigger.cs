using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NodeTrigger : MonoBehaviour
{
    public WorldNode worldNode;

    public bool blocked;

    [Button]
    public void FlipBlocked()
    {
        blocked = !blocked;
        worldNode.OnChangeNodeState(worldNode, blocked);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            blocked = true;
            worldNode.OnChangeNodeState(worldNode, blocked);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        blocked = false;
        worldNode.OnChangeNodeState(worldNode, blocked);
    }
}