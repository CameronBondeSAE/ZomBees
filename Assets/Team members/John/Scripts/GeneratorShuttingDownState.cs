using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;
public class GeneratorShuttingDownState : MonoBehaviour
{
    public AudioClip generatorShuttingDown;
    private AudioSource generatorAudio;
    private bool componentActive = true;
    public float stateSwapTimer;

    private void OnEnable()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorShuttingDownState>());
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorShuttingDown;
        generatorAudio.Play();
        Debug.Log("Phase 3 Begins!");
        Debug.Log("The sound clip playing is "+generatorAudio.clip);
        StartCoroutine(DelayCoroutine(stateSwapTimer));
    }

    private void OnDisable()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorIdleOffState>());
        generatorAudio.Stop();
        Debug.Log("Phase 3 Complete!");
    }
    
    /*public void ToggleActivation()
    {
        componentActive = !componentActive;
        enabled = componentActive;

        if (componentActive)
        {
            OnEnable();
        }
        else
        {
            OnDisable();
        }
    }*/
    
    IEnumerator DelayCoroutine(float amount)
    {
        Debug.Log("Coroutine Ran Succesfully");
        yield return new WaitForSeconds(amount);
        OnDisable();
    }
}
