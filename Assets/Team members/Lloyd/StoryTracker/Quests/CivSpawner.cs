using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivSpawner : MonoBehaviour
{
    public List<Vector2> spawnPoints = new List<Vector2>();
    
    public GameObject civPrefab;

    public TileTracker tileTracker;

    public bool initialized=false;

    [Button]
    public void StartGame(TileTracker newTileTracker)
    {
        foreach (Vector2 spawnPoint in spawnPoints)
        {
            GameObject newCiv = Instantiate(civPrefab, new Vector3(spawnPoint.x, 0, spawnPoint.y), Quaternion.identity) as GameObject;
            PathFinder pathFind = newCiv.GetComponent<PathFinder>();
            Vector2Int spawnPointInt = new Vector2Int((int)spawnPoint.x, (int)spawnPoint.y);
            pathFind.startCoords = spawnPointInt;
            newTileTracker.ChangeSquareType((int)spawnPoint.x, (int)spawnPoint.y, TileTracker.SquareType.Ally);
            pathFind.SetTileTracker(newTileTracker);
            pathFind.StartGame();
        }

        initialized = true;
    }
        
}
