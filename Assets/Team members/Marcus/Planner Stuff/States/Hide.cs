using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using Oscar;

namespace Marcus
{
    public class Hide : AntAIState
    {
        private AdvancedGuyDudeMovement move;
        private OscarVision vision;

        private Vector3 averageEnemyPosition;
        private PatrolPoint chosenPoint;
        
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            move = aGameObject.GetComponentInParent<AdvancedGuyDudeMovement>();
            vision = aGameObject.GetComponentInChildren<OscarVision>();
        }

        public override void Enter()
        {
            base.Enter();
            
            CalculateEnemyAveragePosition();
            
            // If there is a nearby point within a threshold, go there without hesitation
            float distance = Vector3.Distance(transform.position, averageEnemyPosition);

            Collider[] nearbyPoints =
                Physics.OverlapSphere(transform.position, distance / 3f, LayerMask.NameToLayer("Patrol points"));

            if (nearbyPoints.Length > 0)
            {
                chosenPoint = 
                    nearbyPoints[Random.Range(0, nearbyPoints.Length)].GetComponent<PatrolPoint>();
                ChangeTargetPoint();
            }
            // Else, Take the average position of enemies in view
            // Find the nearest point in the opposite direction
            // Go towards it
            else
            {
                /*
                TODO Figure out a calculation for opposite direction
                
                TODO Set "chosenPoint" to a hiding spot in this direction
                chosenPont = 
                
                ChangeTargetPoint();
                */
            }
        }

        public override void Exit()
        {
            base.Exit();

            chosenPoint = null;
            ChangeTargetPoint();
        }

        #region Functions

        void ChangeTargetPoint()
        {
            move.MoveToPoint(chosenPoint);
        }

        void CalculateEnemyAveragePosition()
        {
            Vector3 sum = Vector3.zero;
            foreach (DynamicObject bee in vision.beesInSight)
                sum += bee.transform.position;

            averageEnemyPosition = sum / vision.beesInSight.Count;
        }

        #endregion
    }
}
