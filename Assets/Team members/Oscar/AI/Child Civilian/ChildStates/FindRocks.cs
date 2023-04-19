using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class FindRocks : AntAIState
{
    private LittleGuy littleGuy;

    private float zoomX;
    private float zoomZ;
    
    private float perlin;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        littleGuy = aGameObject.GetComponent<LittleGuy>();

        zoomX = Random.Range(-0.5f, 0.5f);
        zoomZ = Random.Range(-0.5f, 0.5f);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        float x = zoomX + Time.time;
        float z = zoomZ + Time.time;

        perlin = Mathf.PerlinNoise(x, z) * 2 - 1;
        
        littleGuy.rb.AddRelativeForce(Vector3.forward * littleGuy.speed,ForceMode.Acceleration);
        littleGuy.rb.AddRelativeTorque(0,perlin,0);
    }
}
