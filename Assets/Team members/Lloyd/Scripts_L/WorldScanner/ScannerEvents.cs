using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerEvents : MonoBehaviour
{
    public Action<Transform, Vector3Int, Vector3Int> SetGrid;
    
    public void OnSetGrid(Transform position, Vector3Int gridSize, Vector3Int cubeSize)
    {
        SetGrid?.Invoke(position,gridSize,cubeSize);
    }
    
    
    public Action<bool> DrawScan;

    public void OnDrawScan(bool isDrawingGrid)
    {
        DrawScan?.Invoke(isDrawingGrid);
    }
    
    public Action<List<WorldNode>, List<WorldNode>> ChangeNodeState;

    public void OnChangeNodeState(List<WorldNode> open, List<WorldNode> blocked)
    {
        ChangeNodeState?.Invoke(open, blocked);
    }
}
