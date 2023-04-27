using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class BeeStingAttack : AntAIState
    {
        //Attack state rotates the "view" of the bee into an attack state
        //activates a OnTriggerEnter sphere that detect civs thru ICiv
        //as big as radius and lasts for timeActive
        //any civs that collide with the attack sphere have either their HP decreased or Beeness increased depending on the BeeStingType
        //changed by attack amount

        public BeeStingerSensor stingSensor;

        public SphereCollider bodySphere;

        public float timeActive;

        public float attackAmount;

        public float radius;

        public Rigidbody rb;

        public enum BeeStingType
        {
            Attack,
            BeenessIncreaser
        }

        public BeeStingType myType;

        private List<ICiv> civs = new List<ICiv>();

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            stingSensor = aGameObject.GetComponent<BeeStingerSensor>();
            bodySphere = GetComponent<SphereCollider>();
        }


        public override void Enter()
        {
            base.Enter();
            rb = stingSensor.rb;

            stingSensor.seesTarget = false;

            myType = stingSensor.myType;

            bodySphere.isTrigger = true;

            StartCoroutine(Ticking());
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
            stingSensor.viewTransform.position = rb.position;
            stingSensor.viewTransform.rotation = rb.rotation;
        }

        private IEnumerator Ticking()
        {
            yield return new WaitForSeconds(timeActive);

            EndAttack();
        }

        private void OnTriggerEnter(Collider other)
        {
            ICiv civ = other.GetComponent<ICiv>();
            if (civ != null && !civs.Contains(civ))
            {
                civs.Add(civ);
                RunFunctionForCiv(civ);
            }
        }

        private void RunFunctionForCiv(ICiv civ)
        {
            civ.HitByBee(myType, attackAmount);
            //Debug.Log("Hit " + civ + " with " + myType + " for " + attackAmount + "!");

            stingSensor.ChangeResources(101);
        }

        private void OnDrawGizmos()
        {
            if (bodySphere)
            {
               // Gizmos.color = Color.red;
                //Gizmos.DrawSphere(transform.position, sphereCollider.radius);
            }
        }

        private void EndAttack()
        {
            stingSensor.viewTransform.rotation = stingSensor.rb.rotation;

            stingSensor.rb.velocity = Vector3.zero;

            //Debug.Log("Finish Attack");

            if (stingSensor.currentResources >= stingSensor.maxResources)
                stingSensor.hasResource = true;

            stingSensor.attacking = false;
            stingSensor.sting = false;
            bodySphere.isTrigger = false;
            Finish();
        }
    }
}