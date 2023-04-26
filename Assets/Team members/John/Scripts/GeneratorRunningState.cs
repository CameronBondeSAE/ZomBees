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
            GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
            GetComponent<GeneratorModel>()?.thingToGivePowerTo?.PoweredOn();
        }

        [Button]
        private void OnDisable()
        {
            generatorAudio.Stop();
            generatorAudio.loop = false;
            GetComponent<GeneratorModel>()?.thingToGivePowerTo?.PoweredOff();
        }

        public void PlaySound()
        {
            generatorAudio.clip = generatorRunning;
            generatorAudio.loop = true;
            generatorAudio.Play();
        }
    }
}

