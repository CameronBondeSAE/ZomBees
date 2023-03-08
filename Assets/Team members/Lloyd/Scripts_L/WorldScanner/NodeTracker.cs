using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;

public class NodeTracker : MonoBehaviour
{
    //NodeTracker keeps track of nodes and turns them on / off

    //list of nodes (weighted by cost)

    public List<WorldNode> worldNodes;

    public WorldNode myNode;

    public List<WorldNode> targetNodes;

    public List<WorldNode> avoidNodes;

    public List<WorldNode> openNodes;
    public List<WorldNode> blockedNodes;

    public L_WorldScanner world;

    private void OnEnable()
    {
        world = GetComponent<L_WorldScanner>();
        SubscribeToNodes();
    }

    private void SubscribeToNodes()
    {
        for (int x = 0; x < worldNodes.Count; x++)
        {
            worldNodes[x].ChangeNodeState += ChangeNodeStateVoid;
        }
    }
    
    public void ChangeNodeStateVoid(WorldNode inputWorldNode, Vector3 nodePos, bool blocked, float nodeCost)
    {
        List<WorldNode> list;
        Vector3 pos = nodePos;

        if (blocked)
            list = blockedNodes;
        else
            list = openNodes;

        //check for node 
        WorldNode existingNode = list.Find(node => node.position == pos);

        if (existingNode != null)
        {
            existingNode.nodeCost = inputWorldNode.nodeCost;
        }
        else
        {
            list.Add(inputWorldNode);
        }

        list.Sort((n1, n2) => -n1.nodeCost.CompareTo(n2.nodeCost));
    }
}