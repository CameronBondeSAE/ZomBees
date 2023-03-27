using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTracker : MonoBehaviour
{
    public const float cubeSize = 1;
    
    const int boardSize = 8;

    public int targetX;
    public int targetY;

    public enum SquareType
    {
        Open,
        Me,
        Blocked,
        Honey,
        Ally,
        Enemy,
        Goal
    }
    public SquareType myType;
    
    
    public SquareType[,] board = new SquareType[boardSize, boardSize];

    private void OnEnable()
    {
        MakeBoard();
    }

    private void MakeBoard()
    {
        for (int x = 0; x < boardSize; x++) {
            for (int y = 0; y < boardSize; y++) {
                Vector3 position = new Vector3(x * cubeSize, 0, y * cubeSize);
            }
        }
    }
    
    public SquareType[,] GetSquareTypesInArea(int centerX, int centerY, int radius)
    {
        int startX = Mathf.Max(0, centerX - radius);
        int endX = Mathf.Min(boardSize - 1, centerX + radius);
        int startY = Mathf.Max(0, centerY - radius);
        int endY = Mathf.Min(boardSize - 1, centerY + radius);
    
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
        return square.x < 0 || square.x >= board.GetLength(0) ||
               square.y < 0 || square.y >= board.GetLength(1);
    }
    
    public void EditorChangeSquareType(int x, int y, SquareType type)
    {
        board[targetX, targetY] = myType;
        SquareTypeChanged?.Invoke(targetX, targetY, myType);

    }
    
    public void ChangeSquareType(int x, int y, SquareType type)
    {
        board[x, y] = type;
        SquareTypeChanged?.Invoke(x, y, type);

    }

    public SquareType GetSquareType(int x, int y)
    {
        return board[x, y];
    }
    
    private static readonly Dictionary<SquareType, Color> SquareTypeToColor = new Dictionary<SquareType, Color>
    {
        { SquareType.Open, Color.white },
        { SquareType.Ally, Color.green },
        { SquareType.Enemy, Color.red },
        { SquareType.Honey, Color.yellow },
        { SquareType.Blocked, Color.black },
        { SquareType.Goal, Color.magenta },
        { SquareType.Me, Color.blue },
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
    
    public delegate void SquareTypeChangedEventHandler(int x, int y, SquareType squareType);
    public event SquareTypeChangedEventHandler SquareTypeChanged;
}