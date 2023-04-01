using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tanks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

namespace Lloyd
{
    public class L_WorldScanner : MonoBehaviour
    {
        public NodeTracker nodeTracker;
        
        public float perlinScale;
        private float randomTime;
        public float minRandTime;
        public float maxRandTime;

        private bool blocked = false;
        private bool negative;

        public Vector3Int offest;

        public GameObject cubePrefab;

        public Vector3Int gridSize;

        public Vector3Int cubeSize;

        public WorldNode[,,] nodes;
        private float nodeCost;

        public List<WorldNode> worldNodes;

        private void OnEnable()
        {
            nodeTracker = GetComponent<NodeTracker>();
            worldNodes = new List<WorldNode>();
            SpawnGrid();
        }

        private void SpawnGrid()
        {
            nodes = new WorldNode[gridSize.x, gridSize.y, gridSize.z];
        }

        [Button]
        public void Scan()
        {
            StartCoroutine(ScanRoutine());
        }

        public IEnumerator ScanRoutine()
        {
            int startX = ((int)Mathf.Floor(gridSize.x / 2f));
            int startY = ((int)Mathf.Floor(gridSize.y / 2f));
            int startZ = ((int)Mathf.Floor(gridSize.z / 2f));

            var nodePos = Vector3Int.zero;

            for (int x = startX; x < startX + gridSize.x; x++)
            {
                nodePos.x = x * cubeSize.x;

                for (int y = startY; y < startY + gridSize.y; y++)
                {
                    nodePos.y = y * cubeSize.y;

                    for (int z = startZ; z < startZ + gridSize.z; z++)
                    {
                        float perlinValue = Mathf.PerlinNoise((x + y + z) * perlinScale, Time.time);
                        float randomTime = Mathf.Lerp(minRandTime, maxRandTime, perlinValue);

                        nodePos.z = z * cubeSize.z;
                        AddNode(nodePos);

                        yield return new WaitForSeconds(randomTime);
                    }
                }
            }
        }
        
        private void AddNode(Vector3Int input)
        {
            if (input.x >= 0 && input.x < gridSize.x * cubeSize.x &&
                input.y >= 0 && input.y < gridSize.y * cubeSize.y &&
                input.z >= 0 && input.z < gridSize.z * cubeSize.z)
            {
                WorldNode node = new WorldNode(input, blocked, nodeCost);
                nodes[(int)node.position.x, (int)node.position.y, (int)node.position.z] = node;

                GameObject nodePrefab =
                    Instantiate(cubePrefab, node.position, Quaternion.identity, transform) as GameObject;

                nodeTracker.allNodes.Add(node);
            }
        }
    }
}