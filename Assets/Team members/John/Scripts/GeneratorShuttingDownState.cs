using System.Collections;
using UnityEngine;

namespace Johns
{
    public class GeneratorShuttingDownState : StateBase
    {
        public AudioClip   generatorShuttingDown;
        public AudioSource generatorAudio;
        public IEnumerator delayCoroutine;

        private void OnEnable()
        {
            generatorAudio.clip = generatorShuttingDown;
            generatorAudio.Play();
            delayCoroutine = DelayCoroutine();
            StartCoroutine(delayCoroutine);
        }

        private void OnDisable()
        {
            StopCoroutine(delayCoroutine);
            generatorAudio.Stop();
        }
    
        IEnumerator DelayCoroutine()
        {
            Debug.Log("Coroutine Ran Succesfully");
            yield return new WaitForSeconds(generatorShuttingDown.length);
            GetComponent<StateManager>().ChangeState(GetComponent<GeneratorIdleOffState>());
        }
    }
}

