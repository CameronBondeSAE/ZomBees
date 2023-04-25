using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class NormalCivMoveToTarget : AntAIState
    {
        //swiped from tutorial folder

        public GameObject owner;
        //NavMeshAgent      navMeshAgent;

        public Rigidbody rb;

        public Transform target;

        //get from stats
        public float moveSpeed;
        public float stopDistance;
        public float decelerationDistance;

        public bool inRange;

        private CivSensor sensor;

        private NormalCivPathFinder pathFinder;

        private CivSensor.MoveType myMoveType;
    
    

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            owner = aGameObject;
            //navMeshAgent = owner.GetComponent<NavMeshAgent>();

            rb = owner.GetComponent<Rigidbody>();

            sensor = owner.GetComponent<CivSensor>();

            pathFinder = owner.GetComponent<NormalCivPathFinder>();
        }

        public override void Enter()
        {
            base.Enter();

            // navMeshAgent.SetDestination(owner.GetComponent<NormalCivBrain>().moveTarget.transform.position);
            decelerationDistance = stopDistance * 3;

            inRange = false;

            CalculateTarget();
        }

        private void CalculateTarget()
        {
            myMoveType = sensor.myMoveType;

            if (myMoveType == CivSensor.MoveType.Searching)
            {
                target = pathFinder.CalculateRandomSafePoint();
            }

            else if (myMoveType == CivSensor.MoveType.WalkToNearestSafePoint)
            {
                target = pathFinder.CalculateNearestSafePoint();
            }

            else if (myMoveType == CivSensor.MoveType.WalkToNearestInteract)
            {
                target = pathFinder.CalculateNearestInteractPoint();
            }

            sensor.ChangeRotateTarget(target);
        }

        private void Stop()
        {
            rb.velocity = Vector3.zero;
            inRange = true;
            owner.GetComponent<CivSensor>().inRange = inRange;
            Finish();
        }

        public override void Exit()
        {
            base.Exit();
            inRange = false;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            if (target == null) return;

            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;

            if (distance <= stopDistance)
            {
                Stop();
            }

            else if (distance <= decelerationDistance)
            {
                float decelerationFactor = Mathf.Clamp01((distance - stopDistance) / (decelerationDistance - stopDistance));
                rb.AddForce(direction.normalized * moveSpeed * decelerationFactor, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(direction.normalized * moveSpeed, ForceMode.Acceleration);
            }

            //if (navMeshAgent.remainingDistance < 1f)
            {
                //  owner.GetComponent<NormalCivBrain>().moveTarget = null;
                Finish();
            }
        }
    }
}