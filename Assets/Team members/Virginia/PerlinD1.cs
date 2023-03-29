using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Virginia;

public class PerlinD1 : MonoBehaviour
{
    public GameObject Cube;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    [Button]
    void Spawn()
    {
        for (int x = 0; x < 100; x++)
        {
            Vector3 Cubes = new Vector3(x,0,0);
            x = x;
            Instantiate(Cube, Cubes, quaternion.identity);
        }
    }
}
