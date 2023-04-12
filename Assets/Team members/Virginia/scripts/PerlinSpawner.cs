using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Virginia;

namespace Virginia
{
    public class PerlinSpawner : MonoBehaviour
    {
        public GameObject BeeHive;

      
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        [Button]
        void Spawn()
        {
            for (int z = 0; z < 100; z++)
            {
                for (int x = 0; x < 100; x++)
                {
                    Vector3 BeeHivepos = new Vector3();
                    BeeHivepos.x = x;
                    BeeHivepos.y = Mathf.PerlinNoise1D(x);
                    BeeHivepos.z = z;
                    Instantiate(BeeHive, BeeHivepos, quaternion.identity);

                }
            }
        }
        
    }
}
