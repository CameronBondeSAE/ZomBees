using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;
public class GeneratorShuttingDownState : MonoBehaviour
{
    public AudioClip   generatorShuttingDown;
    public AudioSource generatorAudio;

    private void OnEnable()
    {
        generatorAudio.clip = generatorShuttingDown;
        generatorAudio.Play();
        StartCoroutine(DelayCoroutine());
    }

    private void OnDisable()
    {
        generatorAudio.Stop();
    }
    
    IEnumerator DelayCoroutine()
    {
        Debug.Log("Coroutine Ran Succesfully");
        yield return new WaitForSeconds(generatorShuttingDown.length);
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorIdleOffState>());
    }
}
