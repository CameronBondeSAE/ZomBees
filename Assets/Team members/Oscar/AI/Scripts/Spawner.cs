using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Spawner : MonoBehaviour
    {
        public GameObject TheGuy;
        private Vector3 spawnLoc;
    
        private void Start()
        {
            spawnLoc = new Vector3(transform.position.x, transform.position.y,transform.position.z);
        }
    
        void Update()
        {
            
        }
    
        private void SpawnGuy()
        {
            Instantiate(TheGuy,spawnLoc,Quaternion.identity);
        }
    }

}
