using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class FindFood : OscarsLittleGuyMovement
    {
        public override void Enter()
        {
            base.Enter();
            
            NavmeshEnabled();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            
            NavmeshFindLocation(PatrolManager.singleton.resourcePoints[Random.Range(0,PatrolManager.singleton.resourcePoints.Count)].transform.position);
        }

        public override void Exit()
        {
            base.Exit();
            
            NavMeshFinish();
        }
    }
}