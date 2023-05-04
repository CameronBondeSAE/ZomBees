using System;
using System.Collections;
using System.Collections.Generic;
using AlexM;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lloyd
{
    public class ZombeeGameManager : MonoBehaviour
    {
        public static ZombeeGameManager Instance { get; private set; }


        public bool autoStart;

        // public GameObject worldTime; 
        // public GameObject newWorldTime;
        public WorldTime newWorldTimeScript;

        public bool NPCOverheadText = true;

        public PlayerModel playerModel;

        // public GameObject chatInterface;

        [Tooltip("Where the top corner of the map should be set to so we skip the empty parts of the map")]
        public Vector3 coordinateOffsetForMapReadingEase;
        
        
        // public GameObject questTracker;
        // public GameObject newQuestTracker;
        // public QuestTracker newQuestTrackerScript;
        //
        // public GameObject tileTracker;
        // public GameObject newTileTracker;
        // private TileTracker newTileTrackerScript;
        //
        // public GameObject civSpawner;
        // public GameObject newCivSpawner;
        // private CivSpawner newCivSpawnerScript;
        //
        // public List<TileLevelMaker> customLevels;
        // private TileLevelMaker loadLevel;
        // private TileTracker.SquareType[,] board;

        private void Awake()
        {
            Instance = this;
            
            // HACK
            playerModel = FindObjectOfType<PlayerModel>();
            
            // chatInterface.SetActive(false);

            if (autoStart)
	        {
		        StartGame();
	        }
        }

        [Button]
        public void StartGame()
        {
            // Vector3 position = transform.position;
            //
            // newWorldTime = Instantiate(worldTime, position, Quaternion.identity) as GameObject;
            // newWorldTimeScript = newWorldTime.GetComponent<WorldTime>();
            //
            // newQuestTracker = Instantiate(questTracker, position, Quaternion.identity) as GameObject;
            // newQuestTrackerScript = newQuestTracker.GetComponent<QuestTracker>();
            //
            // newTileTracker = Instantiate(tileTracker, position, Quaternion.identity) as GameObject;
            // newTileTrackerScript = newTileTracker.GetComponent<TileTracker>();
            //
            // newCivSpawner = Instantiate(civSpawner, position, Quaternion.identity) as GameObject;
            // newCivSpawnerScript = newCivSpawner.GetComponent<CivSpawner>();
        
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
        
            // newQuestTrackerScript.StartGame(newWorldTimeScript);
            // while (!newQuestTrackerScript.initialized)
            // {
            //     yield return null;
            // }
            // Debug.Log("quests set"); 
            //
            // newTileTrackerScript.StartGame();
            // while (!newTileTrackerScript.initialized)
            // {
            //     yield return null;
            // }
            // //LoadLevel();
            // Debug.Log("tiles made");
            //
            // newCivSpawnerScript.StartGame(newTileTrackerScript);
            // while (!newCivSpawnerScript.initialized)
            // {
            //     yield return null;
            // }
            // Debug.Log("civs spawned");
        }
    
        /*public void LoadLevel()
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
        }*/
    
        //subscribe to events later

        public string ConvertWorldSpaceToGridSpace(Vector3 worldSpace)
        {
            // We'll divide the real world by 10 to make it chunky so GPT can figure it out easier maybe
            Vector3 offsetForMapReadingEase = (worldSpace - coordinateOffsetForMapReadingEase) / 20f;
         
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            int index = (int)offsetForMapReadingEase.x - 1;

            char letter = ' ';
            if (index<=25)
            {
                letter = alphabet[index];
            }

            return letter + ((int)offsetForMapReadingEase.z).ToString();
        }

        public Vector3 ConvertGridSpaceToWorldSpace(string gridSpace)
        {
            // We'll UNdivide the real world by X to make it real

            char letter = char.Parse(gridSpace.Substring(0,1));
            int x = letter - 'A' + 1;
            int z = int.Parse(gridSpace.Substring(1, 1));
            Vector3 worldSpace = new Vector3(x, 0, z) * 20f + new Vector3(coordinateOffsetForMapReadingEase.x, 0, coordinateOffsetForMapReadingEase.z);
            
            return worldSpace;
        }
    }
}