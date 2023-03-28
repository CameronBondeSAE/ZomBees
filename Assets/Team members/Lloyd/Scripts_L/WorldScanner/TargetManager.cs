using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<WorldNode> openNodes;
    public List<WorldNode> blockedNodes;

    public WorldNode startNodes;

    public WorldNode endNode;

    private ScannerEvents scanEvent;
    
    private void Start()
    {
        scanEvent = GetComponent<ScannerEvents>();

        scanEvent.ChangeNodeState += ChangeNodeState;
    }

    private void ChangeNodeState(List<WorldNode> open, List<WorldNode> blocked)
    {
        openNodes.Clear();
        blockedNodes.Clear();

        openNodes = openNodes;
        blockedNodes = blocked;
    }
    

}
