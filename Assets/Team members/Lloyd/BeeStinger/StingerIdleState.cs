using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Team_members.Lloyd.BeeStinger;
using UnityEngine;

public class StingerIdleState : AntAIState
{
    public BeeStingerSensor stingSensor;

    public bool heardSound;
    public bool seesTarget;

    public Vector3 homePoint;
    public float forceMagnitude;

    public Rigidbody rb;

    public StingerRandom stingerRandom;
    public RotateAway rotate;

    public CivVision civVision;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        stingSensor = aGameObject.GetComponent<BeeStingerSensor>();
    }

    public override void Enter()
    {
        base.Enter();
        homePoint = stingSensor.homePoint;
        rb = stingSensor.rb;

      //  rotate = GetComponent<RotateAway>();
      //  rotate.StartSpin(rb, homePoint);

      //  stingerRandom = GetComponent<StingerRandom>();
      //  stingerRandom.StartRandom(rb);

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
            Debug.Log("Saw target: " + stingSensor.attackTarget);
            stingSensor.seesTarget = true;
            Finish();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}