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
            // Point to model variable, take off Time.fixedDeltaTime
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

