using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    //Chat GPT assisted script
    
    public TileTracker tileTracker;
    public Vector2Int startCoords;
    public Vector2Int targetCoords;

    public Vector2Int currentCoords;

    private List<Vector2Int> SquaresToBeScanned = new List<Vector2Int>();
    private List<Vector2Int> SquaresScanned = new List<Vector2Int>();

    private int cubeSize;
    
    //where to set values? so not hard coded

    public int allyNeighbourCost=3;
    public int enemyNeighbourCost=3;
    
    private Dictionary<TileTracker.SquareType, int> SquareTypeCosts = new Dictionary<TileTracker.SquareType, int>()
    {
        {TileTracker.SquareType.Me, Int32.MaxValue},
        
        { TileTracker.SquareType.Open, 5 },
        { TileTracker.SquareType.Blocked, int.MaxValue },
        { TileTracker.SquareType.Ally, int.MaxValue },
        { TileTracker.SquareType.Goal, 1 },
        { TileTracker.SquareType.Honey, 4 },
        { TileTracker.SquareType.Enemy, int.MaxValue },
        { TileTracker.SquareType.SafeRoom, 1 }
    };

    private Dictionary<Vector2Int, int> SquareCosts = new Dictionary<Vector2Int, int>();
    private Dictionary<Vector2Int, Vector2Int> SquareParents = new Dictionary<Vector2Int, Vector2Int>();

    public void SetTileTracker(TileTracker newTileTracker)
    {
        tileTracker = newTileTracker;
    }

    public void StartGame()
    {
        cubeSize = (int)tileTracker.CubeSize();
        tileTracker.ChangeSquareType(startCoords.x, startCoords.y, TileTracker.SquareType.Me);
        tileTracker.ChangeSquareType(targetCoords.x, targetCoords.y, TileTracker.SquareType.Goal);
        tileTracker.SquareTypeChanged += OnSquareTypeChanged;
    }
    
    private void OnSquareTypeChanged(int x, int y, TileTracker.SquareType squareType)
    {
        FindPath();
    }

    [Button]
    public void ChangeTargetCoords(Vector2Int newTarget)
    {
        startCoords = currentCoords;
        targetCoords = newTarget;
        
        FindPath();
    }

    public void SetNewCurrent(Vector2Int newPost, Vector2Int oldPost)
    {
        currentCoords = newPost;
        tileTracker.ChangeSquareType(newPost.x, newPost.y, TileTracker.SquareType.Me);
        tileTracker.ChangeSquareType(oldPost.x, oldPost.y, TileTracker.SquareType.Open);

    }

    [Button]
    public void FindPath()
    {
        SquaresToBeScanned.Clear();
        SquaresScanned.Clear();
        SquareCosts.Clear();
        SquareParents.Clear();

        SquaresToBeScanned.Add(startCoords);
        SquareCosts[startCoords] = 0;
        SquareParents[startCoords] = startCoords;
        
        publicPath.Clear();

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
                    SquaresScanned.Contains(neighbour))
                {
                    continue;
                }

                int newCost = SquareCosts[current] +
                              SquareTypeCosts[tileTracker.GetSquareType(neighbour.x, neighbour.y)];

                if (!SquareCosts.ContainsKey(neighbour) || newCost < SquareCosts[neighbour])
                {
                    if (tileTracker.GetSquareType(neighbour.x, neighbour.y) == TileTracker.SquareType.Open)
                    {
                        int neighbourCost;
                        if (tileTracker.GetSquareType(neighbour.x, neighbour.y) == TileTracker.SquareType.Enemy)
                        {
                            neighbourCost = enemyNeighbourCost;
                            newCost += neighbourCost;
                        }

                        else if (tileTracker.GetSquareType(neighbour.x, neighbour.y) == TileTracker.SquareType.Ally
                                 || tileTracker.GetSquareType(neighbour.x, neighbour.y) == TileTracker.SquareType.SafeRoom)
                        {
                            neighbourCost = allyNeighbourCost;
                            newCost -= neighbourCost;
                        }
                    }

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
                publicPath.Add(new Vector3Int(square.x*cubeSize, 0, square.y*cubeSize));
            }
            DrawPath(path);
        }
    }

    public List<Vector3Int> publicPath = new List<Vector3Int>();

    private List<Vector2Int> GetNeighbours(Vector2Int square)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                Vector2Int neighbour = square + new Vector2Int(x, y);

                if (!tileTracker.IsOutOfBounds(neighbour) &&
                    tileTracker.GetSquareType(square.x+x,square.y+y) != TileTracker.SquareType.Blocked)
                {
                    neighbours.Add(neighbour);
                }
            }
        }

        return neighbours;

        /*List<Vector2Int> neighbours = new List<Vector2Int>();
        neighbours.Add(new Vector2Int(square.x + 1, square.y));
        neighbours.Add(new Vector2Int(square.x - 1, square.y));
        neighbours.Add(new Vector2Int(square.x, square.y + 1));
        neighbours.Add(new Vector2Int(square.x, square.y - 1));
        return neighbours;*/
    }
    
    private void DrawPath(List<Vector2Int> path)
    {
        Debug.DrawLine(Vector3.zero, Vector3.zero, Color.clear);

        foreach (Vector2Int square in path)
        {
            Vector3 pos = new Vector3(square.x*cubeSize, 0, square.y*cubeSize);
            Debug.DrawLine(pos, pos, Color.clear, float.MaxValue);
        }

        if (path.Count < 2) return;

        Vector2Int start = path[0];
        Vector3 startPos = new Vector3(start.x*cubeSize, 0, start.y*cubeSize);
        Vector2Int next = path[1];
        Vector3 endPos = new Vector3(next.x*cubeSize, 0, next.y*cubeSize);
        Debug.DrawLine(startPos, endPos, Color.red, 1f);

        for (int i = 1; i < path.Count - 1; i++)
        {
            Vector2Int current = path[i];
            next = path[i + 1];

            startPos = new Vector3(current.x*cubeSize, 0, current.y*cubeSize);
            endPos = new Vector3(next.x*cubeSize, 0, next.y*cubeSize);

            Debug.DrawLine(startPos, endPos, Color.red, 1f);
        }
    }

    public void OnDisable()
    {
        tileTracker.SquareTypeChanged -= OnSquareTypeChanged;
    }
}