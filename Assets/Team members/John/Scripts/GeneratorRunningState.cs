using Sirenix.OdinInspector;
using UnityEngine;


namespace Johns
{
    public class GeneratorRunningState : StateBase
    {
        public AudioClip   generatorRunning;
        public AudioSource generatorAudio;

<<<<<<< Updated upstream
    private void OnEnable()
    {
        PlaySound();
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
        
        GetComponent<GeneratorModel>()?.thingToGivePowerTo.TurnOn();
    }
    
    [Button]
    private void OnDisable()
    {
        generatorAudio.Stop();
        generatorAudio.loop = false;
        GetComponent<GeneratorModel>().thingToGivePowerTo.TurnOff();
    }
=======
        private void OnEnable()
        {
            PlaySound();
            GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
        }
    
        [Button]
        private void OnDisable()
        {
            generatorAudio.Stop();
            generatorAudio.loop = false;
        }
>>>>>>> Stashed changes
    
        public void PlaySound()
        {
            generatorAudio.clip = generatorRunning;
            generatorAudio.loop = true;
            generatorAudio.Play();
        }
    } 
}

