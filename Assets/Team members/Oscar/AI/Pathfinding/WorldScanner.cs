using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Cinemachine;
using Tanks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Color = UnityEngine.Color;

public class WorldScanner : MonoBehaviour
{
    public Vector3Int size;
    public Vector3Int gridSize;
    public bool[,] gridNodeReferences;
    
    //public bool[,] grid;

    //public bool isBlocked;

    // Start is called before the first frame update
    private void Awake()
    {
        gridNodeReferences = new bool[size.x,size.z];
    }

    void Update()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.z; z++)
            {
                if (Physics.OverlapBox(new Vector3(x * gridSize.x, 0, z * gridSize.z),
                        new Vector3(gridSize.x, gridSize.y, gridSize.z) / 2f, Quaternion.identity).Length > 0)
                {
                    // Something is there
                    gridNodeReferences[x, z] = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {
                if (gridNodeReferences[x, z])
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one);
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one);
                }
            }
        }
    }
}
