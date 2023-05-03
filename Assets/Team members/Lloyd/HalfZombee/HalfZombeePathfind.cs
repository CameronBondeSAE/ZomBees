using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfZombeePathfind : MonoBehaviour
{
    //coauthored with chatGPT

    public int maxPatrolPointsToCheck;
    public List<PatrolPoint> patrolPoints;

    public List<PatrolPoint> hivePoints;
    
    public Transform finalTarget;

    public PatrolPoint lastViablePatrolPoint;

    public void Start()
    {
        patrolPoints = new List<PatrolPoint>(PatrolManager.singleton.paths);
        hivePoints = new List<PatrolPoint>(PatrolManager.singleton.hivePoints);
    }

    public void SortPoints(List<PatrolPoint> listToSort)
    {
        listToSort.Sort(CompareDistances);
    }

    int CompareDistances(PatrolPoint a, PatrolPoint b)
    {
        float distA = Vector3.Distance(transform.position, a.transform.position);
        float distB = Vector3.Distance(transform.position, b.transform.position);

        return distA.CompareTo(distB);
    }

    public void SeekPath(List<PatrolPoint> pointsList)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, finalTarget.transform.position - transform.position, out hit))
        {
            if (hit.collider.gameObject == finalTarget)
            {
                //either see target or return
                return;
            }
        }

        SortPoints(pointsList);

        for (int i = 0; i < pointsList.Count; i++)
        {
            PatrolPoint obj = pointsList[i];
            Vector3 direction = obj.transform.position - transform.position;

            if (Physics.Raycast(transform.position, direction, out hit, 100, 255, QueryTriggerInteraction.Collide))
            {
                if (hit.collider)
                {
                    lastViablePatrolPoint = obj;
                    return;
                }
            }
        }
    }
}