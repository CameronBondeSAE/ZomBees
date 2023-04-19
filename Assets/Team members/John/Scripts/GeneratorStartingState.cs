using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using Sirenix.OdinInspector;
using UnityEngine;
public class GeneratorStartingState : MonoBehaviour
{
    public  AudioClip   generatorStartUp;
    public  AudioSource generatorAudio;

    [Button]
    public void OnEnable()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
        generatorAudio.clip = generatorStartUp;
        generatorAudio.Play();
        StartCoroutine(DelayCoroutine());
    }

    public void OnDisable()
    {
        generatorAudio.Stop();
    }

    IEnumerator DelayCoroutine()
    {
        Debug.Log("Coroutine Ran Succesfully");
        yield return new WaitForSeconds(generatorStartUp.length);
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
    }
}
