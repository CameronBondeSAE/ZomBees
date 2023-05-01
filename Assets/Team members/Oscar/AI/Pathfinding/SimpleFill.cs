using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Oscar
{
    public class SimpleFill : MonoBehaviour
    {
        public WorldScanner worldScanner;

        public GameObject startObj;
        public GameObject endObj;
        
        private Nodes startNode;
        private Nodes endNode;
        private Nodes currentNode;

        private List<Nodes> filledNodes = new List<Nodes>();

        [Button]
        public void FloodFill()
        {
            startNode = worldScanner.gridNodeReferences[(int)startObj.transform.position.x, (int)startObj.transform.position.z];
            endNode = worldScanner.gridNodeReferences[(int)endObj.transform.position.x, (int)endObj.transform.position.z];
            currentNode = startNode;

            Fill(startNode, endNode);
        }

        void Fill(Nodes node, Nodes endNode)
        {            
            currentNode = node;

            if (node == endNode) {
                filledNodes.Add(node);
            }
            
            node.isFilled = true;
            filledNodes.Add(node);

            foreach (Nodes neighbour in GetNeighbours(node))
            {
                if (neighbour.isFilled)
                {
                    Fill(neighbour, endNode);
                }
            }
        }

        List<Nodes> GetNeighbours(Nodes node)
        {
            List<Nodes> neighbors = new List<Nodes>();

            //check right
            if (node.gridPos.x + 1 < worldScanner.size.x)
            {
                neighbors.Add(worldScanner.gridNodeReferences[node.gridPos.x + 1, node.gridPos.y]);
            }
            
            //check left
            if (node.gridPos.x - 1 >= 0)
            {
                neighbors.Add(worldScanner.gridNodeReferences[node.gridPos.x - 1, node.gridPos.y]);
            }
            
            //check up
            if (node.gridPos.y + 1 < worldScanner.size.z)
            {
                neighbors.Add(worldScanner.gridNodeReferences[node.gridPos.x, node.gridPos.y + 1]);
            }
            
            //check down
            if (node.gridPos.y - 1 >= 0)
            {
                neighbors.Add(worldScanner.gridNodeReferences[node.gridPos.x, node.gridPos.y - 1]);

            }
            
            return neighbors;
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                foreach (Nodes node in filledNodes)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(node.worldPos, Vector3.one);
                }

                if (currentNode != null)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawCube(currentNode.worldPos, Vector3.one);
                }

                if (endNode != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(endNode.worldPos, Vector3.one);
                }
            }
        }

    }
}