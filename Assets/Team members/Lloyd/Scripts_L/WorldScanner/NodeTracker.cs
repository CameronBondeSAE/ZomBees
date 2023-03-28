using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;

public class NodeTracker : MonoBehaviour
{
    //NodeTracker keeps track of nodes and if they're open/blocked

    //list of nodes (weighted by cost)

    public WorldNode myNode;

    public WorldNode targetNodes;

    public List<WorldNode> allNodes = new List<WorldNode>();
    
    public List<WorldNode> avoidNodes;

    public List<WorldNode> openNodes;
    public List<WorldNode> blockedNodes;
    
    public void GridArray()
    {
        foreach (WorldNode node in allNodes)
        {
            node.ChangeNodeAction += ChangeNodeStateVoid;
            node.OnChangeNodeState(node, false);
        }
    }
    
    public void ChangeNodeStateVoid(WorldNode inputWorldNode,bool blocked)
    {
        List<WorldNode> list;

        if (blocked)
        {
            list = blockedNodes;
            inputWorldNode.isBlocked = true;
        }
        else
        {
            list = openNodes;
            inputWorldNode.isBlocked = false;
        }

        //check for node 
        WorldNode existingNode = list.Find(node => node.position == inputWorldNode.position);

        if (existingNode != null)
        {
            existingNode.nodeCost = inputWorldNode.nodeCost;
        }
        else
        {
            list.Add(inputWorldNode);
        }

        //list.Sort((n1, n2) => -n1.nodeCost.CompareTo(n2.nodeCost));
    }
}