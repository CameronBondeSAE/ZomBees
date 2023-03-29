using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Level Maker", menuName = "Tile Level")]
public class TileLevelMaker : ScriptableObject
{
    public string LevelMaker;

    public const float cubeSize = 1;
    
    const int boardSize = 16;

    private TileEventArgs tileEventArgs;
    
    public TileTracker.SquareType[,] board = new TileTracker.SquareType[boardSize, boardSize];

    public TileTracker.SquareType[,] previousBoard;

    [Button]
    public void SaveSettings()
    {
        //SaveSystem.Save(this);
        previousBoard = board;
    }
    
    [Button]
    public void LoadSettings()
    {
        //SaveSystem.Load(this);
        board = previousBoard;
    }
    [Button]
    public TileTracker.SquareType[,] Board()
    {
        return board;
    }
}
