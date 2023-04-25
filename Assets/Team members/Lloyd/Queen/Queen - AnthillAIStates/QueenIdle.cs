using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{

    public class QueenIdle : AntAIState
    {
        private QueenScenarioManager queenScene;

        public float minIdleTime;
        public float maxIdleTime;

        public float waitTime;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);

            queenScene = aGameObject.GetComponent<QueenScenarioManager>();

            float randomIdleTime = Random.Range(minIdleTime, maxIdleTime);
            waitTime = randomIdleTime;

            StartCoroutine(IdleWait());
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            aDeltaTime = Time.deltaTime;
            aTimeScale = 1;
            base.Execute(aDeltaTime, aTimeScale);
        }

        public override void Enter()
        {
            base.Enter();
        }

        private IEnumerator IdleWait()
        {
            yield return new WaitForSeconds(waitTime);
            Exit();
        }

        public override void Exit()
        {
            queenScene.idle = false;
        }
    }
}