using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Lloyd;

public class NodePath : MonoBehaviour
{
    private L_WorldScanner worldScanner;

    public List<WorldNode> openNodes;

    public List<WorldNode> openNeighbours;

    private Vector3 cubeSize;

    private Vector3 gridSize;

    [Button]
    private void GetList()
    {
        worldScanner = GetComponent<L_WorldScanner>();

        cubeSize = worldScanner.cubeSize;
        gridSize = worldScanner.gridSize;

        openNodes = worldScanner.worldNodes;
    }

    [Button]
    private void ScanForNeighbours(Vector3 currentPosition)
    {
        foreach (WorldNode openNode in openNodes)
        {
            if (openNode.position == currentPosition)
            {
                continue; // Skip the current position
            }

            if (Mathf.Abs(openNode.position.x - currentPosition.x) <= 1 &&
                Mathf.Abs(openNode.position.y - currentPosition.y) <= 1)
            {
                openNeighbours.Add(openNode);
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                Vector3 nodePosition = new Vector3(z * cubeSize.x, 0, z * cubeSize.z);
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawCube(nodePosition, cubeSize);
                }
            }
        }
    }
}