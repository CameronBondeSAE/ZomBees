using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sirenix.OdinInspector;
using Tanks;
using UnityEngine;

namespace Lloyd
{
    public class L_WorldScanner : MonoBehaviour
    {
        public bool drawingGrid = false;
        public float scanTime;

        public Vector3Int gridSize;

        public Vector3Int cubeSize;

        public WorldNode[,] nodes;

        public List<WorldNode> openNodes = new List<WorldNode>();
        public List<WorldNode> closeNodes = new List<WorldNode>();

        public List<WorldNode> openNeighbours = new List<WorldNode>();

        private void OnEnable()
        {
            SpawnGrid();
        }

        private void SpawnGrid()
        {
            nodes = new WorldNode[gridSize.x, gridSize.z];
        }

        [Button]
        public void Scan()
        {
            if (!drawingGrid)
                StartCoroutine(ScanCoRoutine());
        }

        private IEnumerator ScanCoRoutine()
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int z = 0; z < gridSize.z; z++)
                {
                    if (x < 0 || x >= nodes.GetLength(0) || z < 0 || z >= nodes.GetLength(1))
                    {
                        continue;
                    }

                    Vector3 nodePosition = new Vector3(x * cubeSize.x, 0, z * cubeSize.z);

                    Collider[] colliders = Physics.OverlapBox(nodePosition,
                        new Vector3(gridSize.x / 2f, gridSize.y / 2f, gridSize.z / 2f), Quaternion.identity);

                    WorldNode node = new WorldNode(nodePosition, colliders.Length > 0);

                    nodes[x, z] = node;

                    if (node.isBlocked)
                    {
                        closeNodes.Add(node);
                    }
                    else
                    {
                        openNodes.Add(node);
                    }
                }
            }

            yield return new WaitForSeconds(scanTime);
            drawingGrid = false;
        }

        private void OnDrawGizmos()
        {
            if (drawingGrid)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    for (int z = 0; z < gridSize.y; z++)
                    {
                        Vector3 nodePosition = new Vector3(x * cubeSize.x, 0, z * cubeSize.z);

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
            }
        }
    }
}