using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Johns
{

    public class GeneratorStartingState : StateBase
    {
        public  AudioClip   generatorStartUp;
        public  AudioSource generatorAudio;
        public IEnumerator coroutine;
    
        [Button]
        public void OnEnable()
        {
            generatorAudio.clip = generatorStartUp;
            generatorAudio.Play();
            coroutine = DelayCoroutine();
            StartCoroutine(coroutine);
        }

        public void OnDisable()
        {
            StopCoroutine(coroutine);
            generatorAudio.Stop();
        }

        IEnumerator DelayCoroutine()
        {
            GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
            yield return new WaitForSeconds(generatorStartUp.length);
        }
    }
}


