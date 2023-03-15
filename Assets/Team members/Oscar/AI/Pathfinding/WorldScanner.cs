using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;
using Color = UnityEngine.Color;

public class WorldScanner : MonoBehaviour
{
    public Vector3Int size;
    public Vector3Int gridSize;

    public Oscar.Nodes[,] gridNodeReferences;
    public LayerMask layerMask;
    
    public Vector3 currentPos;
    public bool[,] grid;

    private bool firstScan;
    
    public void ScanWorld()
    {
        gridNodeReferences = new Nodes[size.x, size.z];
        
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.z; z++)
            {
                gridNodeReferences[x, z] = new Nodes();
                gridNodeReferences[x, z].gridPos = new Vector2Int(x, z);
                gridNodeReferences[x, z].worldPos = new Vector3(x, 0, z);

                if (Physics.OverlapBox(/*transform.position + */new Vector3(x * gridSize.x, 0, z * gridSize.z),
                        new Vector3(gridSize.x, 0, gridSize.z)/2f, Quaternion.identity).Length > 0)
                {
                    gridNodeReferences[x, z].isBlocked = true;
                }
                else
                {
                    gridNodeReferences[x, z].isBlocked = false;
                }
            }
        }
        firstScan = true;
    }
    
    private void OnDrawGizmos()
    {
        if (Application.isPlaying && firstScan == true)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int z = 0; z < size.y; z++)
                {
                    if (gridNodeReferences[x, z].isBlocked)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(x, 1, z), Vector3.one);
                    }
                    else
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawCube(new Vector3(x, 1, z), Vector3.one);
                    }
                }
            }
        }
    }
}
