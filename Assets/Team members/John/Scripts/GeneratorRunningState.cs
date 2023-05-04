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

        private void OnEnable()
        {
            PlaySound();
            generator = GetComponent<GeneratorModel>();
            if (generator != null)
            {
                foreach (IPowered poweredObject in generator.thingToGivePowerTo)
                {
                    poweredObject.PoweredOn();
                }
            }
        }

        void FixedUpdate()
        {
            Collider[] things = new Collider[] { };
            // model radius var
            Physics.OverlapSphereNonAlloc(transform.position, 3f, things, Int32.MaxValue, QueryTriggerInteraction.Ignore);
            // Point to model variable, take off Time.fixedDeltaTime

            foreach (Collider item in things)
            {
                // Do stuff to item.GetComponent<IPowered>().power....blah
            }
        }

        [Button]
        private void OnDisable()
        {
            generatorAudio.Stop();
            generatorAudio.loop = false;
            GeneratorModel generator = GetComponent<GeneratorModel>();
            if (generator != null)
            {
                foreach (IPowered poweredObject in generator.thingToGivePowerTo)
                {
                    poweredObject.PoweredOff();
                }
            }
        }

        public void PlaySound()
        {
            generatorAudio.clip = generatorRunning;
            generatorAudio.loop = true;
            generatorAudio.Play();
        }
    }
}

