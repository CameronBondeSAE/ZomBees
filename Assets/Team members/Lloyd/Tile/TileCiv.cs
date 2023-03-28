using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.Experimental;
using UnityEngine;

public class TileCiv : MonoBehaviour

{
    public PathFinder pathfinder;

    public TileTracker tileTracker;

    public bool moving=false;
    
    public Vector3 target;
    
    public float minDist;

    public float moveSpeed;

    public Rigidbody rb;
    
    public List<Vector3Int> pathList;

    public void WaitForPath()
    {
        
    }

    [Button]
    public void SetPath()
    {
        tileTracker = pathfinder.tileTracker;
        pathList = new List<Vector3Int>(pathfinder.publicPath);
        target = pathList[0];
        moving = true;
    }

    public void FixedUpdate()
    {
        if(moving)
        Move();
    }

    public void Move()
    {
            if (pathList.Count == 0 || target == null)
            {
                return;
            }
            
            float distToTargetSqr = (rb.transform.position - target).sqrMagnitude;

            if (distToTargetSqr < (minDist + 0.001f) * (minDist + 0.001f))
            {
                int newX = pathList[0].x;
                int newY = pathList[0].y;
                tileTracker.ChangeSquareType(newX, newY, TileTracker.SquareType.Me);

                if (pathList.Count > 1)
                {
                    int oldX = pathList[1].x;
                    int oldY = pathList[1].y;
                    tileTracker.ChangeSquareType(oldX, oldY, TileTracker.SquareType.Open);
                }

                pathfinder.currentCoords = new Vector2Int(pathList[0].x,pathList[0].y);

                pathList.RemoveAt(0);

                if (pathList.Count > 0)
                {
                    target = pathList[0];
                }
                else
                {
                    moving = false;
                    rb.velocity = Vector3.zero;
                    return;
                }

                /**/
                /*Vector3 direction = target - rb.transform.position;
                direction.Normalize();
                Vector3 force = direction * moveSpeed;
                rb.AddForce(force, ForceMode.Acceleration);*/
                
                Vector3 newPos = Vector3.Slerp(rb.transform.position, target, Time.deltaTime * moveSpeed / distToTargetSqr);
                rb.MovePosition(newPos);
            }
            
            
    }
}