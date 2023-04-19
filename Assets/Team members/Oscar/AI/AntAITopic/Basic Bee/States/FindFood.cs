using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Oscar;
using Random = UnityEngine.Random;

public class FindFood : AntAIState
{
    private LittleGuy littleGuy;
    
    public float zoomX;
    public float zoomZ;
    
    public float perlin;
    public float sine;

    private bool attackBool;
    
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        zoomX = Random.Range(-0.5f, 0.5f);
        zoomZ = Random.Range(-0.5f, 0.5f);
        
        littleGuy = aGameObject.GetComponent<LittleGuy>();
    }

    public override void Enter()
    {
        base.Enter();

        littleGuy.GetComponentInChildren<ColourChangeShader>().attackPhase = false;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        float x = zoomX + Time.time;
        float z = zoomZ + Time.time;

        perlin = Mathf.PerlinNoise(x, z) * 2 - 1;
        
        littleGuy.rb.AddRelativeForce(Vector3.forward * littleGuy.speed,ForceMode.Acceleration);
        littleGuy.rb.AddRelativeTorque(0,perlin * littleGuy.turnSpeed,0);
    }
}
