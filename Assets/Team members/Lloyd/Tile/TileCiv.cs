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

    public bool waitingOnGoal=true;
    
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
        pathList = new List<Vector3Int>(pathfinder.publicPath);
        waitingOnGoal = false;
        target = pathList[0];
        waitingOnGoal = false;
    }

    public void FixedUpdate()
    {
        if(!waitingOnGoal)
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
                pathfinder.currentCoords = new Vector2Int(pathList[0].x,pathList[0].y);
                
                /*int oldX = pathList[0].x;
                int oldY = pathList[0].y;
                tileTracker.ChangeSquareType(oldX, oldY, TileTracker.SquareType.Open);
                                         
                int newX = pathList[1].x;
                int newY = pathList[1].y;
                tileTracker.ChangeSquareType(newX, newY, TileTracker.SquareType.Me);*/

                pathList.RemoveAt(0);

                if (pathList.Count > 0)
                {
                    target = pathList[0];
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    waitingOnGoal = true;
                    
                    return;
                }

                /**/
            }
            else
            {
                /*Vector3 direction = target - rb.transform.position;
                direction.Normalize();
                Vector3 force = direction * moveSpeed;
                rb.AddForce(force, ForceMode.Acceleration);*/
                
                Vector3 newPos = Vector3.Slerp(rb.transform.position, target, Time.deltaTime * moveSpeed / distToTargetSqr);
                rb.MovePosition(newPos);
            }
            
            
    }
}