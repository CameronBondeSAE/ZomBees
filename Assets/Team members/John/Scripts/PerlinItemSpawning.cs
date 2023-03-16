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
    public float height;
    
    private List<GameObject> spawnedItems = new List<GameObject>();
    
    // Start is called before the first frame update
    void Awake()
    {
        height = Random.Range(0f, 200f);
    }

    
    [Button]
    void SpawnItems()
    {
        float y = Mathf.PerlinNoise(height, 0);
        
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 instantiatePosition = new Vector3(x, y, z);
                spawnedItems.Add(Instantiate(item, instantiatePosition, quaternion.identity));
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
