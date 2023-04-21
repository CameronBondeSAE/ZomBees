using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class BeeStingAttack : AntAIState
{
    public BeeStingerSensor stingSensor;
    
    public float timeActive;

    public float radius;
    
    public enum BeeStingType
    {
        Attack,
        BeenessIncreaser
    }

    public BeeStingType myType;
    
    private SphereCollider sphereCollider;
    private List<ICiv> civs = new List<ICiv>();

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        stingSensor = aGameObject.GetComponent<BeeStingerSensor>();
    }


    public override void Enter()
    {
        base.Enter();

        stingSensor.seesTarget = false;
        
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = radius;

        StartCoroutine(Ticking());
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        sphereCollider.center = transform.position;
        stingSensor.viewTransform.position = transform.position;
    }

    private IEnumerator Ticking()
    {
        yield return new WaitForSeconds(timeActive);

        EndAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        ICiv civ = other.GetComponent<ICiv>();
        if (civ != null && !civs.Contains(civ))
        {
            civs.Add(civ);
            RunFunctionForCiv(civ);
        }
    }

    private void RunFunctionForCiv(ICiv civ)
    {
        Debug.Log("Hit "+civ);
        stingSensor.ChangeResources(101);
    }
    
    private void OnDrawGizmos()
    {
        if (sphereCollider)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, sphereCollider.radius);
        }
    }

    private void EndAttack()
    {
        
        stingSensor.viewTransform.rotation = stingSensor.rb.rotation;

        stingSensor.rb.velocity = Vector3.zero;
        
        Debug.Log("Finish Attack");

        if (stingSensor.currentResources >= stingSensor.maxResources)
            stingSensor.hasResource = true;
        
        stingSensor.attacking = false;
        Finish();
    }
}
