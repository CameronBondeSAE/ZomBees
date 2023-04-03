using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TileTracker : MonoBehaviour
{
    //Chat GPT assisted script
    
    public bool initialized=false;
    
    public const float cubeSize = 10;

    public float CubeSize()
    {
        return cubeSize;
    }
    
    const int boardSize = 16;

    public int targetX;
    public int targetY;
    public int targetZ;

    [InlineEditor()]
    public TileLevelMaker scriptObj;
    
    public enum SquareType
    {
        Open,
        Me,
        Blocked,
        Honey,
        Ally,
        Enemy,
        Goal,
        
        SafeRoom
    }
    public SquareType myType;
    
    public SquareType[,] board = new SquareType[boardSize, boardSize];

    public void StartGame()
    {
        board = scriptObj.Board();
        MakeBoard();
    }
    
    private void MakeBoard()
    {
        for (int x = 0; x < boardSize; x++) {
            for (int y = 0; y < boardSize; y++) {
                Vector2 position = new Vector3(x * cubeSize, 0, y * cubeSize);
            }
        }

        initialized = true;
    }
    
    public SquareType[,] GetSquareTypesInArea(int centerX, int centerZ, int radius)
    {
        int startX = Mathf.Max(0, centerX - radius);
        int endX = Mathf.Min(boardSize - 1, centerX + radius);
        int startY = Mathf.Max(0, centerZ - radius);
        int endY = Mathf.Min(boardSize - 1, centerZ + radius);
    
        SquareType[,] area = new SquareType[endX - startX + 1, endY - startY + 1];
    
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                area[x - startX, y - startY] = board[x, y];
            }
        }
    
        return area;
    }

    public bool IsOutOfBounds(Vector2Int square)
    {
        return square.x < 0 || square.x  >= board.GetLength(0) ||
               square.y  < 0  || square.y  >= board.GetLength(1);
    }
    
    /*public void EditorChangeSquareType(int x, int y, SquareType type)
    {
        board[targetX, targetY] = myType;
        SquareTypeChanged?.Invoke(targetX, targetY, myType);
    }*/

    public void ChangeSquareType(int x, int y, SquareType type)
    {
        board[x, y] = type;
        SquareTypeChanged?.Invoke(x, y, type);
    }
    
    [Button]
    public void ChangeSquareTypeInRange(int startX, int startY, int endX, int endY, SquareType type)
    {
        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                board[i, j] = type;
                SquareTypeChanged?.Invoke(i, j, type);
            }
        }
    }

    public SquareType GetSquareType(int x, int y)
    {
        return board[x, y];
    }
    
    public delegate void SquareTypeChangedEventHandler(int x, int y, SquareType squareType);
    public event SquareTypeChangedEventHandler SquareTypeChanged;

    #region ViewShit

    private static readonly Dictionary<SquareType, Color> SquareTypeToColor = new Dictionary<SquareType, Color>
    {
        { SquareType.Open, Color.gray },
        { SquareType.Ally, Color.white },
        { SquareType.Enemy, Color.red },
        { SquareType.Honey, Color.yellow },
        { SquareType.Blocked, Color.black },
        { SquareType.Goal, Color.magenta },
        { SquareType.Me, Color.blue },

        { SquareType.SafeRoom, Color.green}
    };

    private void OnDrawGizmos()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                Gizmos.color = SquareTypeToColor[board[x, y]];
                Vector3 position = new Vector3(x * cubeSize, 0, y * cubeSize);
                Gizmos.DrawCube(position, new Vector3(cubeSize, 0, cubeSize));
            }
        }
    }
    
    
    #endregion
}