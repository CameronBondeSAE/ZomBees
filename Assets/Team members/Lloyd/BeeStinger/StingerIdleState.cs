using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class StingerIdleState : AntAIState
    {
        public BeeStingerSensor stingSensor;
        public ShaderGraphChangeColor shader;

        public Vector3 homePoint;
        //public float forceMagnitude;

        public Rigidbody rb;

        public StingerRandom stingerRandom;
       // public RotateAway rotate;

        public Tether tether;
       // public IdleRotate idleRotate;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            stingSensor = aGameObject.GetComponent<BeeStingerSensor>();
            shader = aGameObject.GetComponentInChildren<ShaderGraphChangeColor>();
        }

        public override void Enter()
        {
            base.Enter();
        
            shader.ChangeColorGreen();
        
            homePoint = stingSensor.homePoint;
            rb = stingSensor.rb;
        
            stingSensor.beeWings.ChangeBeeWingStats(-90, 15,true);

            //idleRotate = GetComponent<IdleRotate>();
           // idleRotate.StartRotate(homePoint, rb);

            tether = GetComponent<Tether>();
            tether.StartTether(rb, homePoint);

        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            if (stingSensor.seesTarget || stingSensor.heardSound)
            {
                stingSensor.idle = false;
                Finish();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}