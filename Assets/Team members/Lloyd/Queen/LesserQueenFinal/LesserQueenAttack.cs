using System.Collections;
using Anthill.AI;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{
    public class LesserQueenAttack : AntAIState
    {
        public float attackTime;
        
        public BeeStingerSensor stingerSensor;

        public LesserQueenSensor queenSensor;

        public GameObject zombeeStinger;
        public GameObject zombeenessIncreaser;

        public QueenEvent queenEvent;

        public Transform attackTarget;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            queenSensor = aGameObject.GetComponent<LesserQueenSensor>();
            queenEvent = aGameObject.GetComponent<QueenEvent>();
        }

        public override void Enter()
        {
            base.Enter();
            attackTarget = queenSensor.attackTarget;
            queenSensor.beeWings.ChangeBeeWingStats(-90, 50, true);
            if (queenSensor.beeBullets <= 0)
            {
                queenSensor.attack = false;
                Finish();
            }
            else
            {
                queenSensor.attack = true;
                StartCoroutine(Attack());
            }
        }

        private IEnumerator Attack()
        {
            int randomIndex = Random.Range(0, 2);

            GameObject selectedObject = randomIndex == 0 ? zombeeStinger : zombeenessIncreaser;
            
            GameObject newObject = Instantiate(selectedObject, new Vector3(0,0,0), quaternion.identity);
            newObject.transform.position = transform.position;
            newObject.transform.rotation = transform.rotation;

            Rigidbody rb = newObject.GetComponent<Rigidbody>();
            Vector3 direction = queenSensor.attackTarget.position - transform.position;
            rb.AddForce(direction.normalized * 100, ForceMode.Impulse);

            LesserQueenLookAt newLook = newObject.GetComponent<LesserQueenLookAt>();
            newLook.target = queenSensor.attackTarget;
            stingerSensor = newObject.GetComponent<BeeStingerSensor>();
            stingerSensor.SetHomePoint(queenSensor.transform.position);
            queenSensor.agitated = true;
            
            queenEvent.OnChangeSwarmPoint(queenSensor.attackTarget);

            queenSensor.beeBullets--;

            yield return new WaitForSeconds(attackTime);

            queenSensor.attack = false;
            Finish();
        }
        
    }
}