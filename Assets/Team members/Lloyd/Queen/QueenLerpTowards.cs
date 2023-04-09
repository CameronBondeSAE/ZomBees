using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using Sirenix.OdinInspector;
using Tanks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Random = UnityEngine.Random;

public class QueenLerpTowards : AntAIState
{
    // Queen has a List of GameObjects flyPoints which is as large as numFlyPoints
    // Queen get s a reference to the next flyPoint and stores it as currentFlyPoint, and the old previousFlyPoint

    // Queen uses LerpTowards to move itself to the currPoint. when it is within the float minDist, it changes points

    // multiple Lists for variable paths?

    private float flyTime;

    public List<GameObject> flyPoints;

    public GameObject prevFlyPoint;

    public GameObject currFlyPoint;

    public float curSpeed;
    private float maxSpeed;
    private float minDist;
    [ReadOnly]
    private bool isMoving=false;

    private QueenScenarioManager queenScene;

    private Rigidbody rb;

    private LookAtTarget lookAt;

    public bool interrupted=false;

    public bool initialised = false;

    private QueenScenarioManager.QueenStates currstate;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        rb = aGameObject.GetComponent<Rigidbody>();
        queenScene = aGameObject.GetComponent<QueenScenarioManager>();
        currstate = queenScene.currState;
        
        foreach (GameObject points in queenScene.patrolPoints)
        {
            flyPoints.Add(points);
        }

        ChooseNewFlyPoint();
        isMoving = true;
        initialised = true;
        StartCoroutine(MoveTowards());
    }

    public override void Execute(float whoCares, float whoCares1)
    {
        if (initialised)
        {
            curSpeed = rb.velocity.magnitude;

            if (isMoving)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            lookAt.SetTarget(currFlyPoint.transform);
        }

        if (interrupted)
            isMoving = false;
    }

    private IEnumerator MoveTowards()
    {
        while (isMoving)
        {
            float journeyLength = Vector3.Distance(transform.position, currFlyPoint.transform.position);
            while (!Mathf.Approximately(journeyLength, 0f) && journeyLength > minDist)
            {
                Vector3 direction = (currFlyPoint.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, currFlyPoint.transform.position);

                float forceMagnitude = Mathf.Clamp(distance / Time.fixedDeltaTime, flyTime, maxSpeed);
                Vector3 force = direction * forceMagnitude;

                rb.AddForce(force, ForceMode.VelocityChange);

                yield return null;
                journeyLength = Vector3.Distance(transform.position, currFlyPoint.transform.position);
                
                if (Vector3.Distance(transform.position, currFlyPoint.transform.position) < minDist)
                {
                    ChooseNewFlyPoint();
                }
            }

            if (flyPoints.Count > 1)
            {
                int currIndex = flyPoints.IndexOf(currFlyPoint);
                currFlyPoint = flyPoints[(currIndex + 1) % flyPoints.Count];
            }
        }
    }
    
    private void ChooseNewFlyPoint()
    {
        int index = Random.Range(0, flyPoints.Count);
        while (flyPoints[index] == prevFlyPoint)
        {
            index = Random.Range(0, flyPoints.Count);
        }
        prevFlyPoint = currFlyPoint;
        currFlyPoint = flyPoints[index];
    }
}