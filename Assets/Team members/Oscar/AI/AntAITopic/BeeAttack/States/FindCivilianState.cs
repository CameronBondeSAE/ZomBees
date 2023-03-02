using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class FindCivilianState : AntAIState
{
    
    public LayerMask Enemies;

    public Collider collide;

    public Vector3 enemyPos; 

    public List<Transform> enemyList = new List<Transform>();
    
    public Oscar.Neighbours neighbours;

    public override void Enter()
    {
        base.Enter();

        /*if (neighbours.enemyList != null)
        {
            Finish();
        }
        else
        {
            print("idk");
        }*/
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        // if (neighbours.enemyList != null)
        // {
        //     Finish();
        // }
        // else
        // {
        //     print("idk");
        // }
        
        Finish();
    }

    public override void Exit()
    {
        
        base.Exit();
        Finish();
    }
}
