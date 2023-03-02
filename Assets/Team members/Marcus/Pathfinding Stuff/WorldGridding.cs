using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Marcus
{
    public class WorldGridding : MonoBehaviour
    {
        public Vector3Int gridOffset;
        
        public Vector3Int worldSize;
        public Vector3Int gridSize;
        
        private bool[,] gridNodeReferences;
        
        private void Awake()
        {
            gridNodeReferences = new bool[worldSize.x,worldSize.z];
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int x = 0; x < worldSize.x; x++)
            {
                for (int z = 0; z < worldSize.z; z++)
                {
                    if (Physics.OverlapBox(new Vector3(x * gridSize.x, 0, z * gridSize.z) + gridOffset,
                            new Vector3(gridSize.x, gridSize.y, gridSize.z)/2, Quaternion.identity).Length > 0)
                    {
                        // Something is there
                        gridNodeReferences[x, z]/*.isBlocked*/ = true;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                for (int x = 0; x < worldSize.x; x++)
                {
                    for (int z = 0; z < worldSize.z; z++)
                    {
                        if (gridNodeReferences[x, z]/*.isBlocked*/)
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawCube(new Vector3(x, 0, z) + gridOffset, Vector3.one);
                        }
                    }
                }
            }
        }
    }
}
