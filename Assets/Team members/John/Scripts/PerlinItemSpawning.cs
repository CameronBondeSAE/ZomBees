using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinItemSpawning : MonoBehaviour
{
    public GameObject item;

    public int length;
    public int width;

    public float offsetX;
    public float offsetZ;

    public float scale;
    
    public float threshold;

    private List<GameObject> spawnedItems = new List<GameObject>();

    [Button]
    void SpawnItems()
    {
        ClearItems();
        
        offsetX = Random.Range(-10000f, 10000f);
        offsetZ = Random.Range(-10000f, 10000f);
        
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float perlinValue = Mathf.PerlinNoise(x * scale * offsetX, z * scale * offsetZ);
                Vector3 setPosition = new Vector3(x, perlinValue, z) + transform.position;
                
                if (perlinValue > threshold)
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(setPosition, Vector3.down, out hitInfo))
                    {
                        int groundLayer = LayerMask.NameToLayer("Ground");
                        if (hitInfo.transform.gameObject.layer == groundLayer)
                        {
                            Vector3 instantiatePosition = new Vector3(setPosition.x, hitInfo.point.y, setPosition.z);
                            spawnedItems.Add(Instantiate(item, instantiatePosition, quaternion.identity));  
                        }
                        else
                        {
                            Debug.Log("an item tried to hit the ground but there was an obstacle");
                        }
                    }
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
