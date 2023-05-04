using Sirenix.OdinInspector;
using System;
using UnityEngine;


namespace Johns
{
    public class GeneratorRunningState : StateBase
    {
        public AudioClip   generatorRunning;
        public AudioSource generatorAudio;
        GeneratorModel     generator;
        public float detectRadius;
        public float rateOfConsumption = 1;
        Collider[] things = new Collider[] { };

        private void OnEnable()
        {
            PlaySound();
        }

        public void FixedUpdate()
        {
            //Detect any Physics objects in a radius and and add them to an Array
            Physics.OverlapSphereNonAlloc(transform.position, detectRadius, things, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            
            // Point to model variable, take off Time.fixedDeltaTime
            GetComponent<GeneratorModel>().currFuel -= rateOfConsumption * Time.fixedDeltaTime;

            //loop through all the items in that array and power them on!
            foreach (Collider item in things)
            {
                item.GetComponent<IPowered>().PoweredOn();
            }
        }

        [Button]
        private void OnDisable()
        {
            StopSound();
            
            //loop through all items in the items array and power them off
            if (generator != null)
            {
                foreach (Collider item in things)
                {
                    item.GetComponent<IPowered>().PoweredOff();
                }
            }
        }

        public void PlaySound()
        {
            generatorAudio.clip = generatorRunning;
            generatorAudio.loop = true;
            generatorAudio.Play();
        }

        public void StopSound()
        {
            generatorAudio.loop = false;
            generatorAudio.Stop();
        }
    }
}

