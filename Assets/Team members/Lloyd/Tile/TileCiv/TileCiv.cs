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

    public float maxSpeed;

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

                if (pathList[1] != null && pathList[0] != null)
                {
                    int newX = pathList[1].x;
                    int newZ = pathList[1].z;
                    Debug.Log("NEW : " + newX + " , " + newX);

                    int oldX = pathList[0].x;
                    int oldZ = pathList[0].z;
                    Debug.Log("OLD : " + oldZ + " , " + oldZ);

                    pathfinder.SetNewCurrent(new Vector2Int(newX, newZ), new Vector2Int(oldX, oldZ));

                    pathList.RemoveAt(0);
                }

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
            
            Vector3 newPosition = Vector3.Slerp(rb.transform.position, target, Time.deltaTime * moveSpeed / distToTargetSqr);
            Vector3 direction = newPosition - rb.transform.position;
            direction.Normalize();
            Vector3 attractionForce = direction * moveSpeed / Time.deltaTime - rb.velocity;
            rb.AddForce(attractionForce, ForceMode.Force);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            
            }
            
            
    }