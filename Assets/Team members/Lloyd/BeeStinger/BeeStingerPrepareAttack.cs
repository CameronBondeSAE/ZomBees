using System.Collections;
using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class StingerPrepareAttack : AntAIState
    {
        public LesserQueenLookAt look;
        
        public float timer;
        public float timerThresh;
        private bool ticking;
    
        public BeeStingerSensor sensor;
        public ShaderGraphChangeColor shaderScript;

        public float raycastLength;
        public float forceMagnitude;

        public Rigidbody rb;

        private bool shooting;
        private bool pissedOff;
        public float pissedOffFloat;
        public float pissedOffThresh;

        public Transform attackTarget;

        public Transform viewTransform;
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            sensor = aGameObject.GetComponent<BeeStingerSensor>();
            shaderScript = aGameObject.GetComponentInChildren<ShaderGraphChangeColor>();
            look = aGameObject.GetComponent<LesserQueenLookAt>();
        }

        public override void Enter()
        {
            base.Enter();

            look.target = attackTarget;
            
//            shaderScript.ChangeColorRed();
        
            pissedOffFloat = 0;
            pissedOff = false;

            timer = 0;
            ticking = true;
            StartCoroutine(Ticking());
        
            attackTarget = sensor.attackTarget;
            rb = sensor.rb;
        
            sensor.beeWings.ChangeBeeWingStats(-125,125,true);

            viewTransform = sensor.viewTransform;

            MoveToSpot();
        }

        public IEnumerator Ticking()
        {
            while (ticking)
            {
                timer++;
                yield return new WaitForSeconds(1);

                if(!pissedOff){

                if (timer > timerThresh)
                {
                    sensor.seesTarget = false;
                    sensor.idle = true;
                    ticking = false;
                    Finish();
                }
            }
            }
        }

        private void MoveToSpot()
        {
            if (attackTarget)
            {
                Quaternion targetRotation = Quaternion.LookRotation(attackTarget.transform.position);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,
                    5 * Time.fixedDeltaTime));
                shooting = true;
            }
        }


        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);

            if (shooting)
            {
                Vector3 targetDirection = attackTarget.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 5 * Time.fixedDeltaTime));

                Ray ray = new Ray(transform.position, attackTarget.position - transform.position);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, raycastLength))
                {
                    if (hitInfo.collider != attackTarget.GetComponent<Collider>())
                    {
                        return;
                    }

                    ICivilian civ = hitInfo.collider.gameObject.GetComponent<ICivilian>();
                    if (civ != null)
                    {
                        pissedOffFloat++;
                    }
                }

                if (pissedOffFloat > pissedOffThresh)
                {
                    pissedOff = true;
                    sensor.sting = true;
                    ticking = false;
                    StartCoroutine(LerpRotation(-70f, 3f));
                }
            }
        }

        public IEnumerator LerpRotation(float targetRotation, float duration)
        {
            shooting = false;
            float elapsedTime = 0;
            float startRotation = rb.rotation.eulerAngles.x;

            while (elapsedTime < duration)
            {
                float rotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime / duration);
                viewTransform.localRotation = Quaternion.Euler(rotation, rb.rotation.y, rb.rotation.z);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Kamikaze();
        }

        private void Kamikaze()
        {
            Vector3 direction = attackTarget.position - transform.position;
            rb.AddForce(direction.normalized * forceMagnitude, ForceMode.Impulse);

            sensor.attacking = true;
            Finish();
        }
    }
}