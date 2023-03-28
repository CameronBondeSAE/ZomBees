using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ScannerView : MonoBehaviour
{
    private Transform centerNode;
    public bool drawingGrid;
    public Vector3Int gridSize;
    public Vector3Int cubeSize;
    public WorldNode[,] nodes;
    private ScannerEvents scannerEvents;

    private void Start()
    {
        scannerEvents = GetComponent<ScannerEvents>();
        scannerEvents.SetGrid += OnSetGrid;
            //scannerEvents.DrawScan += OnDrawGizmos;
    }

    private void OnSetGrid(Transform center, Vector3Int grid, Vector3Int cube)
    {
        centerNode = center;
        gridSize = grid;
        cubeSize = cube;
        nodes = new WorldNode[gridSize.x, gridSize.z];
    }

    /*private void OnDrawGizmos(bool input)
    {
        drawingGrid = input;
        if (drawingGrid && nodes != null)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    Vector3 nodePosition = nodes[x, z].position;

                    if (nodes[x, z].isBlocked)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(nodePosition, cubeSize);
                    }
                    else
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawCube(nodePosition, cubeSize);
                    }
                }
            }
        }*/
    }
