using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinItemSpawning : MonoBehaviour
{
    public GameObject item;

    public int length = 0;
    public int width = 0;

    public float perlinLowerLimit;
    public float perlinUpperLimit;
    
    private List<GameObject> spawnedItems = new List<GameObject>();
    
    private void Update()
    {
        perlinLowerLimit = Random.Range(0f, -1f);
        perlinUpperLimit = Random.Range(0f, 1f);
    }

    [Button]
    void SpawnItems()
    {
        ClearItems();
        
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float perlinValue = Mathf.PerlinNoise(x*perlinLowerLimit, z * perlinUpperLimit);
                Vector3 instantiatePosition = new Vector3(x, perlinValue, z);
                if (instantiatePosition.y > 0.9f)
                {
                    spawnedItems.Add(Instantiate(item, instantiatePosition, quaternion.identity)); 
                }
            }
        }
    }
    
    [Button]
    void ClearItems()
    {
        foreach(GameObject item in spawnedItems)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
    }
}
