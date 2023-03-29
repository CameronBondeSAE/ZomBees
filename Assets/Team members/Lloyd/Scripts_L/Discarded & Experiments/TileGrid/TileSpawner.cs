/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public enum TileTeam
    {
        Neutral,
        Human,
        Zombee
    }

    public List<GameObject> objectsToSpawn;

    public TileTeam team;

    private GameObject[,] squares;

    private void Start()
    {
        squares = TileGrid.Instance.GetSquares();
      //  SpawnObjects();
    }
}
/*private void SpawnObjects()
{
    foreach (GameObject objectToSpawn in objectsToSpawn)
    {
     //   GameObject spawnedObject = Instantiate(objectToSpawn);
      //  ChessPiece chessPiece = spawnedObject.GetComponent<ChessPiece>();

        // Set the team of the spawned ChessPiece
      //  chessPiece.SetTeam(team);

        // Get the square to spawn the ChessPiece on
      //  int x = Random.Range(0, ChessBoard.Instance.boardSize);
      //  int y = Random.Range(0, ChessBoard.Instance.boardSize);
      //  GameObject square = squares[x, y];

        // Spawn the ChessPiece on the square
//        }
}
}
#1#
*/
