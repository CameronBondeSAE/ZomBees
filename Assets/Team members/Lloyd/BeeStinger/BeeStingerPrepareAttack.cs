using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;
using UnityEngine.Analytics;

public class StingerAttackState : AntAIState
{
    public float timer;
    public float timerThresh;
    private bool ticking;
    
    public BeeStingerSensor sensor;

    public float raycastLength = 10f;
    public float forceMagnitude = 10f;

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
    }

    public override void Enter()
    {
        base.Enter();

        timer = 0;
        ticking = true;
        StartCoroutine(Ticking());

        pissedOffFloat = 0;
        pissedOff = false;

        attackTarget = sensor.attackTarget;
        rb = sensor.rb;

        viewTransform = sensor.viewTransform;

        MoveToSpot();
    }

    public IEnumerator Ticking()
    {
        while (ticking)
        {
            timer++;
            yield return new WaitForSeconds(1);

            if (timer > timerThresh)
            {
                sensor.seesTarget = false;
                ticking = false;
                Finish();
            }
        }
    }

    private void MoveToSpot()
    {
        Quaternion targetRotation = Quaternion.LookRotation(attackTarget.transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,
            5 * Time.fixedDeltaTime));
        shooting = true;
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
                    Debug.Log("hit something");
                    return;
                }

                ICiv civ = hitInfo.collider.gameObject.GetComponent<ICiv>();
                if (civ != null)
                {
                    Debug.Log("target Sighted");
                    pissedOffFloat++;
                }
            }

            if (pissedOffFloat > pissedOffThresh)
            {
                pissedOff = true;
                ticking = false;
                StartCoroutine(LerpRotation(-70f, 3f));
            }
        }
    }

    public IEnumerator LerpRotation(float targetRotation, float duration)
    {
        float elapsedTime = 0;
        float startRotation = rb.rotation.eulerAngles.x;

        while (elapsedTime < duration)
        {
            float rotation = Mathf.Lerp(startRotation, targetRotation, elapsedTime / duration);
            viewTransform.rotation = Quaternion.Euler(rotation, 0f, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Kamikaze();
    }

    private void Kamikaze()
    {
        rb.AddForce(transform.forward * forceMagnitude, ForceMode.Impulse);
    }
}