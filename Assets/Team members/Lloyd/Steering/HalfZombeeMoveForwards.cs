using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Lloyd
{


    public class HalfZombeeMoveForwards : MonoBehaviour
    {
        //chatGPT WAS HERE

        public CivVision civVision;

        private Rigidbody rb;

        public HalfZombeeProfile profile;

        public float maxSpeed;

        public float speedMultiplier;

        [ReadOnly] public float currentSpeed;

        public float decreaseAmount;

        public float checkTime;

        public bool running = false;

        public List<GameObject> objs;

        private void OnEnable()
        {
            civVision = GetComponent<CivVision>();
            profile = GetComponent<HalfZombeeProfile>();
            rb = GetComponent<Rigidbody>();
            StartCoroutine(AdjustSpeed());
        }

        private IEnumerator AdjustSpeed()
        {
            while (true)
            {
                objs = new List<GameObject>(civVision.visibleObjects);
                if (objs.Any())
                {
                    float distance = Vector3.Distance(transform.position, objs[0].transform.position);
                    speedMultiplier = distance * decreaseAmount;
                }
                else
                {
                    speedMultiplier = 1;
                }

                yield return new WaitForSeconds(checkTime);
            }
        }

        private void FixedUpdate()
        {
            currentSpeed = profile.currentSpeed;
            //   Debug.Log(rb.velocity);
            ClampSpeed();
            MoveForwards();
        }

        private void ClampSpeed()
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        private void MoveForwards()
        {
            rb.AddRelativeForce(Vector3.forward * (currentSpeed * speedMultiplier));
        }
    }
}