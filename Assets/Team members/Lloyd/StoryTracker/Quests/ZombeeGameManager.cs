using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;
using Sirenix.OdinInspector;

public class ZombeeGameManager : MonoBehaviour
{
    public static ZombeeGameManager Instance { get; private set; }

    public GameObject worldTime; 
    public GameObject newWorldTime;
    private WorldTime newWorldTimeScript;

    public GameObject questTracker;
    public GameObject newQuestTracker;
    private QuestTracker newQuestTrackerScript;

    public GameObject tileTracker;
    public GameObject newTileTracker;
    private TileTracker newTileTrackerScript;

    public GameObject civSpawner;
    public GameObject newCivSpawner;
    private CivSpawner newCivSpawnerScript;

    public List<TileLevelMaker> customLevels;
    private TileLevelMaker loadLevel;
    private TileTracker.SquareType[,] board;
    
    [Button]
    public void StartGame()
    {
        Vector3 position = transform.position;
        
        newWorldTime = Instantiate(worldTime, position, Quaternion.identity) as GameObject;
        newWorldTimeScript = newWorldTime.GetComponent<WorldTime>();

        newQuestTracker = Instantiate(questTracker, position, Quaternion.identity) as GameObject;
        newQuestTrackerScript = newQuestTracker.GetComponent<QuestTracker>();

        newTileTracker = Instantiate(tileTracker, position, Quaternion.identity) as GameObject;
        newTileTrackerScript = newTileTracker.GetComponent<TileTracker>();

        newCivSpawner = Instantiate(civSpawner, position, Quaternion.identity) as GameObject;
        newCivSpawnerScript = newCivSpawner.GetComponent<CivSpawner>();
        
        StartCoroutine(Begin());
    }

    private IEnumerator Begin()
    {
        newWorldTimeScript.StartGame();
        while (!newWorldTimeScript.initialized)
        {
            yield return null;
        }
        Debug.Log("timer started"); 
        
        newQuestTrackerScript.StartGame(newWorldTimeScript);
        while (!newQuestTrackerScript.initialized)
        {
            yield return null;
        }
        Debug.Log("quests set"); 
        
        newTileTrackerScript.StartGame();
        while (!newTileTrackerScript.initialized)
        {
            yield return null;
        }
        LoadLevel();
        Debug.Log("tiles made");
            
        newCivSpawnerScript.StartGame(newTileTrackerScript);
        while (!newCivSpawnerScript.initialized)
        {
            yield return null;
        }
        Debug.Log("civs spawned");
    }
    
    public void LoadLevel()
    {
        loadLevel = customLevels[0];
        board = loadLevel.Board();
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                newTileTrackerScript.ChangeSquareType(x, y, board[x,y]);
            }
        }
    }
    
    //subscribe to events later
    
}