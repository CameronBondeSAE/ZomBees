using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinNoiseSpawner : MonoBehaviour
{
    public GameObject hiveCube;

    public int length = 0;
    public int width = 0;
    public float height;
    

    private List<GameObject> spawnedThings = new List<GameObject>();

    private void Awake()
    {
        height = Random.Range(0f, 200f);
    }

    [Button]
    void SpawnCubes()
    {
        float y = Mathf.PerlinNoise(height, 0);
        
            for (int z = 0; z < length; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3 instantiatePosition = new Vector3(x, y, z);
                    spawnedThings.Add(Instantiate(hiveCube, instantiatePosition, quaternion.identity));
                }
            }
    }

    [Button]
    void ClearCubes()
    {
        foreach(GameObject item in spawnedThings)
        {
            Destroy(item);
        }
        spawnedThings.Clear();
    }
}
