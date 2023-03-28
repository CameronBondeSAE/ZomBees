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
    
    public GameObject questTracker;
    public GameObject newQuestTracker;

    public GameObject tileTracker;
    public GameObject newTileTracker;

    public GameObject civSpawner;
    public GameObject newCivSpawner;

    [Button]
    public void StartGame()
    {
        newWorldTime = Instantiate(worldTime, transform.position, Quaternion.identity) as GameObject;

        newQuestTracker = Instantiate(questTracker, transform.position, Quaternion.identity) as GameObject;

        newTileTracker = Instantiate(tileTracker, transform.position, Quaternion.identity) as GameObject;

        newCivSpawner = Instantiate(civSpawner, transform.position, Quaternion.identity) as GameObject;
        
        StartCoroutine(Begin());
    }

    private IEnumerator Begin()
    {
        newWorldTime.GetComponent<WorldTime>().StartGame();
        while (!newWorldTime.GetComponent<WorldTime>().initialized)
        {
            yield return null;
        }
        Debug.Log("timer started"); 
        
        newQuestTracker.GetComponent<QuestTracker>().StartGame(newWorldTime.GetComponent<WorldTime>());
        while (!newQuestTracker.GetComponent<QuestTracker>().initialized)
        {
            yield return null;
        }
        Debug.Log("quests set"); 
        
        newTileTracker.GetComponent<TileTracker>().StartGame();
        while (!newTileTracker.GetComponent<TileTracker>().initialized)
        {
            yield return null;
        }
        Debug.Log("tiles made");
            
        newCivSpawner.GetComponent<CivSpawner>().StartGame(newTileTracker.GetComponent<TileTracker>());
        while (!newCivSpawner.GetComponent<CivSpawner>().initialized)
        {
            yield return null;
        }
        Debug.Log("civs spawned");
    }
    
    //subscribe to events
    
}