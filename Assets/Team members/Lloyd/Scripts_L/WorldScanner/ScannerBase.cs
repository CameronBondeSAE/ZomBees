using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerBase
{
    public List<WorldNode> openList;
    public List<WorldNode> blockedList;
    public List<WorldNode> neighbourNodes;

    private WorldNode closestNodes;

    public ScannerEvents scannerEvent;
    
    public bool drawingGrid;
    
    public Vector3Int gridSize;

    public Vector3Int cubeSize;
    
    public WorldNode[,] nodes;
}
