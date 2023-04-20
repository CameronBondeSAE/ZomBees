using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class GetFood : OscarsLittleGuyMovement
    {
        private OscarVision vision;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            vision = aGameObject.GetComponentInChildren<OscarVision>();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            if (vision.foodInSight.Count > 0)
            {
                BasicMovement(1f);
                TurnTowards(vision.foodInSight[0].transform.position);
            }
        }
    }
}