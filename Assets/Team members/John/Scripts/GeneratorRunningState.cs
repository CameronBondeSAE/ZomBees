using Sirenix.OdinInspector;
using UnityEngine;


namespace Johns
{
    public class GeneratorRunningState : StateBase
    {
        public AudioClip generatorRunning;
        public AudioSource generatorAudio;

        private void OnEnable()
        {
            PlaySound();
            GeneratorModel generator = GetComponent<GeneratorModel>();
            if (generator != null)
            {
                foreach (IPowered poweredObject in generator.thingToGivePowerTo)
                {
                    poweredObject.PoweredOn();
                }
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

