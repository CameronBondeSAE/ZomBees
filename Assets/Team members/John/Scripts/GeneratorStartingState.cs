using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using Sirenix.OdinInspector;
using UnityEngine;
public class GeneratorStartingState : MonoBehaviour
{
    public AudioClip generatorStartUp;
    private AudioSource generatorAudio;
    private bool componentActive = true;
    public float stateSwapTimer;

    [Button]
    public void OnEnable()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorStartUp;
        generatorAudio.Play();
        Debug.Log("Phase 1 Begins!");
        Debug.Log("The sound clip playing is "+generatorAudio.clip);
        StartCoroutine(DelayCoroutine(stateSwapTimer));
    }

    public void OnDisable()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
        Debug.Log("Phase 1 complete!");
        generatorAudio.Stop();
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
