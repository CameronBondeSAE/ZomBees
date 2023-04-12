using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PerlinItemSpawning1 : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int width = 10;
    [SerializeField] private int length = 10;
    [SerializeField] private float perlinScale = 0.1f;
    [SerializeField] private float perlinThreshold = 0.5f;
    [SerializeField] private float minItemHeight = 0f;
    [SerializeField] private float maxItemHeight = 1f;
    [SerializeField] private int groupSizeThreshold = 5;

    private List<GameObject> spawnedItems = new List<GameObject>();

    [Button]
    private void SpawnItems()
    {
        ClearItems();

        // Adjust perlinScale based on minItemHeight and maxItemHeight
        perlinScale *= (maxItemHeight - minItemHeight) * 2f;

        // Create a 2D array to represent the spawned items
        GameObject[,] itemGrid = new GameObject[width, length];

        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float perlinValue = Mathf.PerlinNoise(x * perlinScale, z * perlinScale);
                if (perlinValue > perlinThreshold)
                {
                    Vector3 position = new Vector3(x, Mathf.Lerp(minItemHeight, maxItemHeight, perlinValue), z);
                    GameObject newItem = Instantiate(itemPrefab, position, Quaternion.identity);
                    spawnedItems.Add(newItem);
                    itemGrid[x, z] = newItem;
                }
            }
        }

        // Iterate over each position in the grid
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                // If there's an item at that position
                if (itemGrid[x, z] != null)
                {
                    // Use flood fill to find connected items
                    HashSet<Vector2Int> connectedItems = FloodFill(itemGrid, x, z);

                    // If the group of connected items is smaller than a certain size
                    if (connectedItems.Count < groupSizeThreshold)
                    {
                        // Destroy all connected items
                        foreach (Vector2Int position in connectedItems)
                        {
                            GameObject item = itemGrid[position.x, position.y];
                            spawnedItems.Remove(item);
                            Destroy(item);
                        }
                    }
                }
            }
        }
    }

    [Button]
    void ClearItems()
    {
        foreach (GameObject item in spawnedItems)
        {
            Destroy(item);
        }

        spawnedItems.Clear();
    }

    // Flood fill algorithm to find connected items
    HashSet<Vector2Int> FloodFill(GameObject[,] grid, int x, int y)
    {
        HashSet<Vector2Int> connectedItems = new HashSet<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(new Vector2Int(x, y));

        while (queue.Count > 0)
        {
            Vector2Int position = queue.Dequeue();
            connectedItems.Add(position);

            if (position.x > 0 && grid[position.x - 1, position.y] != null &&
                !connectedItems.Contains(new Vector2Int(position.x - 1, position.y)))
            {
                queue.Enqueue(new Vector2Int(position.x - 1, position.y));
            }

            if (position.y > 0 && grid[position.x, position.y - 1] != null &&
                !connectedItems.Contains(new Vector2Int(position.x, position.y - 1)))
            {
                queue.Enqueue(new Vector2Int(position.x, position.y - 1));
            }

            if (position.y < grid.GetLength(1) - 1 && grid[position.x, position.y + 1] != null &&
                !connectedItems.Contains(new Vector2Int(position.x, position.y + 1)))
            {
                queue.Enqueue(new Vector2Int(position.x, position.y + 1));
            }
        }

        return connectedItems;
    }
}

    
    


