using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class BeeStingerReturnToHive : AntAIState
    {
        public BeeStingerSensor sensor;

        public ShaderGraphChangeColor shader;
    
        public Vector3 target;
        public Rigidbody rb;
        public float forceMultiplier;
        public float maxDistance;
        public float minDistance;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<BeeStingerSensor>();
            shader = aGameObject.GetComponentInChildren<ShaderGraphChangeColor>();
        }

        public override void Enter()
        {
            base.Enter();
            rb = sensor.rb;
            target = sensor.homePoint;
        
            sensor.GetComponent<BeenessIncreaserModel>().ChangeWings(-175, 12,true);
        
            shader.ChangeColorOrange();
        
            Quaternion targetRotation = Quaternion.LookRotation(target);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,
                5 * Time.fixedDeltaTime));
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            Vector3 direction = target - transform.position;
            float distance = direction.magnitude;

            if (distance > maxDistance)
            {
                float force = distance * forceMultiplier;
                rb.AddForce(direction.normalized * force);
            }
            else if (distance > minDistance)
            {
                float force = Mathf.Lerp(0, maxDistance * forceMultiplier, distance / maxDistance);
                rb.AddForce(direction.normalized * force);
            }
            else
            {
                rb.velocity = Vector3.zero;
                HiveInteract();
            }
        }

        public BeeHive beehive;

        private void HiveInteract()
        {
            int depositAmount = sensor.currentResources;
            beehive = sensor.hivePoint.GetComponent<BeeHive>();
            beehive.ChangeFloat(depositAmount);
            sensor.ChangeResources(-depositAmount);
            sensor.hasResource = false;
            sensor.backToOrigin = true;
            Finish();
        }
    }
}
