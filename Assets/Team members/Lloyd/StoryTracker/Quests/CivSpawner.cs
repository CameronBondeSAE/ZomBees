using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivSpawner : MonoBehaviour
{
    public List<Vector2> spawnPoints = new List<Vector2>();
    
    //add to whatever
    public List<Vector2> targetPoints = new List<Vector2>();
    
    public GameObject civPrefab;

    public GameObject newCiv;

    public TileTracker tileTracker;

    public bool initialized=false;

    public float spawnDelay = 0.5f;
    public float startDelay = 0.5f;
    
    public CharacterCreator charCre;

    public void StartGame(TileTracker newTileTracker)
    {
        charCre = GetComponent<CharacterCreator>();
        StartCoroutine(SpawnCivs(newTileTracker));
    }

    private IEnumerator SpawnCivs(TileTracker newTileTracker)
    {
        yield return new WaitForSeconds(startDelay);

        foreach (Vector2 spawnPoint in spawnPoints)
        {
            newCiv = Instantiate(civPrefab, new Vector3(spawnPoint.x, 0, spawnPoint.y), Quaternion.identity) as GameObject;
            PathFinder pathFind = newCiv.GetComponent<PathFinder>();
            Vector2Int spawnPointInt = new Vector2Int((int)spawnPoint.x, (int)spawnPoint.y);
            pathFind.startCoords = spawnPointInt;
            newTileTracker.ChangeSquareType((int)spawnPoint.x, (int)spawnPoint.y, TileTracker.SquareType.Ally);
            pathFind.SetTileTracker(newTileTracker);
            pathFind.StartGame();
            pathFind.FindPath();
            
            charCre.RandomiseCiv(newCiv);

            yield return new WaitForSeconds(spawnDelay);
        }

        initialized = true;
    }

    [Button]
    public void Begin()
    {
        TileCiv tileCiv = newCiv.GetComponent<TileCiv>();
        tileCiv.SetPath();
    }
        
}
