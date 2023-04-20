using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class GeneratorRunningState : MonoBehaviour
{
    public AudioClip   generatorRunning;
    public AudioSource generatorAudio;

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
    
    public void PlaySound()
    {
        generatorAudio.clip = generatorRunning;
        generatorAudio.loop = true;
        generatorAudio.Play();
    }
}
