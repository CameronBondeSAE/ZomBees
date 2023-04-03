using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using Sirenix.OdinInspector;
using UnityEngine;
public class GeneratorRunningState : MonoBehaviour
{
    public AudioClip generatorRunning;
    private AudioSource generatorAudio;
    private bool componentActive = true;

    private void OnEnable()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorRunning;
        generatorAudio.Play();
    }
    
    [Button]
    private void OnDisable()
    {
        generatorAudio.Stop();
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorShuttingDownState>());
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
    
}
