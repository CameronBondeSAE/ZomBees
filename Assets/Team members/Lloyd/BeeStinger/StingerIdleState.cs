using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using Team_members.Lloyd.BeeStinger;
using UnityEngine;

public class StingerIdleState : AntAIState
{
    public BeeStingerSensor stingSensor;
    public ShaderGraphChangeColor shader;

    public bool heardSound;
    public bool seesTarget;

    public Vector3 homePoint;
    public float forceMagnitude;

    public Rigidbody rb;

    public StingerRandom stingerRandom;
    public RotateAway rotate;

    public Tether tether;

    public CivVision civVision;

    public IdleRotate idleRotate;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        stingSensor = aGameObject.GetComponent<BeeStingerSensor>();
        shader = aGameObject.GetComponentInChildren<ShaderGraphChangeColor>();
    }

    public override void Enter()
    {
        base.Enter();
        seesTarget = false;
        
        shader.ChangeColorGreen();
        
        homePoint = stingSensor.homePoint;
        rb = stingSensor.rb;
        
        stingSensor.ChangeWings(-90, 15,true);

      //  rotate = GetComponent<RotateAway>();
      //  rotate.StartSpin(rb, homePoint);

      //  stingerRandom = GetComponent<StingerRandom>();
      //  stingerRandom.StartRandom(rb);

      idleRotate = GetComponent<IdleRotate>();
      idleRotate.StartRotate(homePoint, rb);

      tether = GetComponent<Tether>();
      tether.StartTether(rb, homePoint);

      civVision = GetComponent<CivVision>();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        seesTarget = civVision.seesCivs;
        stingSensor.heardSound = heardSound;

        if (heardSound || seesTarget)
        {
            stingSensor.SetAttackTarget(civVision.ReturnNearestCiv());
            Debug.Log(civVision.ReturnNearestCiv());
            stingSensor.seesTarget = true;
            stingSensor.idle = false;
            Finish();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}