using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using UnityEngine;
public class GeneratorStartingState : MonoBehaviour
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
        Debug.Log("Coroutine Ran Succesfully");
        yield return new WaitForSeconds(generatorStartUp.length);
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
    }
}
