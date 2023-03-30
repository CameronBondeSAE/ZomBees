using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;
public class GeneratorRunningState : MonoBehaviour
{
    public AudioClip generatorRunning;
    private AudioSource generatorAudio;
    private bool componentActive = true;
    public StateManager stateMachine;

    private void OnEnable()
    {
        stateMachine.ChangeState(this);
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorRunning;
        Debug.Log("Phase 2 Begins!");
        Debug.Log("The sound clip playing is "+generatorAudio.clip);
        generatorAudio.Play();
    }

    private void OnDisable()
    {
        generatorAudio.Stop();
        Debug.Log("Phase 2 Complete!");
    }
    
    public void ToggleActivation()
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
    }
}
