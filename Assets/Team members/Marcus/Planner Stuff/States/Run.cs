using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

namespace Marcus
{
    public class Run : AntAIState
    {
        private OscarVision vision;
        private AdvancedGuyDudeMovement movement;
        
        private Vector3 directionToRun;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            vision = aGameObject.GetComponentInChildren<OscarVision>();
            movement = aGameObject.GetComponentInParent<AdvancedGuyDudeMovement>();
        }

        public override void Enter()
        {
            base.Enter();

            CalcuateDirection();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            if (vision.beesInSight.Count > 0)
                CalcuateDirection();
        }

        private void CalcuateDirection()
        {
            // Take average position of all visible bees
            // Calculate directionToRun and send to movement script
            
            Vector3 sum = Vector3.zero;
            foreach (GameObject bee in vision.beesInSight)
                sum += bee.transform.position;

            Vector3 centrePoint = sum / vision.beesInSight.Count;
            directionToRun = new Vector3(centrePoint.x, 1, centrePoint.z) * 5f;
            
            CallMovement();
        }

        void CallMovement()
        {
            PatrolPoint destination = new PatrolPoint();
            destination.transform.position = directionToRun;
            
            movement.MoveToPoint(destination);
        }

        public override void Exit()
        {
            base.Exit();
            
            movement.MoveToPoint(null);
        }
    }
}
