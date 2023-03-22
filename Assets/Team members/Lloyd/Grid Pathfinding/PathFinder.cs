using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public TileTracker tileTracker;
    public Vector2Int startCoords;
    public Vector2Int targetCoords;

    public int squareSize;

    private List<Vector2Int> SquaresToBeScanned = new List<Vector2Int>();
    private List<Vector2Int> SquaresScanned = new List<Vector2Int>();

    private Dictionary<TileTracker.SquareType, int> SquareTypeCosts = new Dictionary<TileTracker.SquareType, int>()
    {
        { TileTracker.SquareType.Open, 2 },
        { TileTracker.SquareType.Blocked, int.MaxValue },
        { TileTracker.SquareType.Ally, 3 },
        { TileTracker.SquareType.Goal, 1 },
        { TileTracker.SquareType.Honey, 4 },
        { TileTracker.SquareType.Enemy, int.MaxValue }
    };

    private Dictionary<Vector2Int, int> SquareCosts = new Dictionary<Vector2Int, int>();
    private Dictionary<Vector2Int, Vector2Int> SquareParents = new Dictionary<Vector2Int, Vector2Int>();

    public void OnEnable()
    {
        tileTracker.ChangeSquareType(startCoords.x, startCoords.y, TileTracker.SquareType.Me);
        tileTracker.ChangeSquareType(targetCoords.x, targetCoords.y, TileTracker.SquareType.Goal);
        tileTracker.SquareTypeChanged += OnSquareTypeChanged;
    }
    
    private void OnSquareTypeChanged(int x, int y, TileTracker.SquareType squareType)
    {
        PathFind();
    }

    public void PathFind()
    {
        FindPath();
    }

    private void FindPath()
    {
        SquaresToBeScanned.Clear();
        SquaresScanned.Clear();
        SquareCosts.Clear();
        SquareParents.Clear();
        
        SquaresToBeScanned.Add(startCoords);
        SquareCosts[startCoords] = 0;
        SquareParents[startCoords] = startCoords;

        while (SquaresToBeScanned.Count > 0)
        {
            SquaresToBeScanned.Sort((a, b) => SquareCosts[a].CompareTo(SquareCosts[b]));

            
            
            int minCost = int.MaxValue;
            Vector2Int current = new Vector2Int();
            foreach (Vector2Int square in SquaresToBeScanned)
            {
                if (SquareCosts.ContainsKey(square) && SquareCosts[square] < minCost)
                {
                    current = square;
                    minCost = SquareCosts[square];
                }
            }
            
            Vector2Int currentSquare = SquaresToBeScanned[0];
            SquaresToBeScanned.RemoveAt(0);
            SquaresScanned.Add(currentSquare);

            if (current == targetCoords)
            {
                break;
            }

            foreach (Vector2Int neighbour in GetNeighbours(current))
            {
                if (tileTracker.IsOutOfBounds(neighbour) ||
                    tileTracker.GetSquareType(neighbour.x, neighbour.y) == TileTracker.SquareType.Blocked ||
                    tileTracker.GetSquareType(neighbour.x, neighbour.y) == TileTracker.SquareType.Enemy ||
                    SquaresScanned.Contains(neighbour))
                {
                    continue;
                }

                int newCost = SquareCosts[current] +
                              SquareTypeCosts[tileTracker.GetSquareType(neighbour.x, neighbour.y)];

                if (!SquareCosts.ContainsKey(neighbour) || newCost < SquareCosts[neighbour])
                {
                    SquareCosts[neighbour] = newCost;
                    SquareParents[neighbour] = current;

                    if (!SquaresToBeScanned.Contains(neighbour))
                    {
                        SquaresToBeScanned.Add(neighbour);
                    }
                }
            }
        }

        if (SquareParents.ContainsKey(targetCoords))
        {
            List<Vector2Int> path = new List<Vector2Int>();
            Vector2Int current = targetCoords;

            while (current != startCoords)
            {
                path.Add(current);
                current = SquareParents[current];
            }

            path.Add(startCoords);
            path.Reverse();

            foreach (Vector2Int square in path)
            {
                
            }
            DrawPath(path);
        }
    }
    

    private List<Vector2Int> GetNeighbours(Vector2Int square)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        neighbours.Add(new Vector2Int(square.x + 1, square.y));
        neighbours.Add(new Vector2Int(square.x - 1, square.y));
        neighbours.Add(new Vector2Int(square.x, square.y + 1));
        neighbours.Add(new Vector2Int(square.x, square.y - 1));
        return neighbours;
    }
    
    private void DrawPath(List<Vector2Int> path)
    {
        Debug.DrawLine(Vector3.zero, Vector3.zero, Color.clear);

        foreach (Vector2Int square in path)
        {
            Vector3 pos = new Vector3(square.x, 0, square.y);
            Debug.DrawLine(pos, pos, Color.clear, float.MaxValue);
        }

        if (path.Count < 2) return;

        Vector2Int start = path[0];
        Vector3 startPos = new Vector3(start.x, 0, start.y);
        Vector2Int next = path[1];
        Vector3 endPos = new Vector3(next.x , 0, next.y);
        Debug.DrawLine(startPos, endPos, Color.red, 1f);

        for (int i = 1; i < path.Count - 1; i++)
        {
            Vector2Int current = path[i];
            next = path[i + 1];

            startPos = new Vector3(current.x, 0, current.y);
            endPos = new Vector3(next.x, 0, next.y);

            Debug.DrawLine(startPos, endPos, Color.red, 1f);
        }
    }

    public void OnDisable()
    {
        tileTracker.SquareTypeChanged -= OnSquareTypeChanged;
    }
}