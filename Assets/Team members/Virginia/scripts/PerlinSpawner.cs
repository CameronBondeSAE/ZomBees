using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using TreeEditor;
using Virginia;

namespace Virginia
{
    public class PerlinSpawner : MonoBehaviour
    {
        public GameObject BeeHive; // - can be set to any game object don't mind the name
        // Start is called before the first frame update
        void Start()
        {
            
        }
        
        [Button]
        void Spawn() // - this spawns cubes alone the x and z axis
        {
            for (int x = 0; x < 100; x++)
            {
                for (int z = 0; z < 100; z++)
                {
                    if ( x < z)
                    {
                        Vector3 BeeHivepos = new Vector3();
                        BeeHivepos.x = x;
                        BeeHivepos.y = Mathf.PerlinNoise1D(x);
                        BeeHivepos.z = z;
                        
                        RaycastHit hitInfo;
                      
                            if (Physics.Raycast(BeeHivepos, Vector3.down, out hitInfo))
                            {
                                Instantiate(BeeHive, BeeHivepos, quaternion.identity);
                            }
                        
                    }
                }
                
            }
            
        }

        
    }
}
