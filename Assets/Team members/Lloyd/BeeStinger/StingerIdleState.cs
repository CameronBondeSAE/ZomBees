using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class StingerIdleState : AntAIState
    {
        public BeeStingerSensor stingSensor;
        public ShaderGraphChangeColor shader;

        private SphereBob bob;

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
            rb = aGameObject.GetComponent<Rigidbody>();
            stingSensor = aGameObject.GetComponent<BeeStingerSensor>();
            shader = aGameObject.GetComponentInChildren<ShaderGraphChangeColor>();
            //bob = aGameObject.GetComponent<SphereBob>();
        }

        public override void Enter()
        {
            base.Enter();

            //bob.origPos = stingSensor.homePoint;
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            shader.ChangeColorGreen();
        
            homePoint = stingSensor.homePoint;
            rb = stingSensor.rb;
        
            stingSensor.beeWings.ChangeBeeWingStats(-90, 15,true);

            //idleRotate = GetComponent<IdleRotate>();
           // idleRotate.StartRotate(homePoint, rb);

            tether = GetComponent<Tether>();
            tether.StartTether(rb, homePoint);

           // bob.enabled = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            if (stingSensor.seesTarget || stingSensor.heardSound)
            {
                stingSensor.idle = false;
                //bob.enabled = false;    
                Finish();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}