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
    public StateManager stateMachine;

    private void OnEnable()
    {
        stateMachine.ChangeState(this);
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorShuttingDown;
        generatorAudio.Play();
        Debug.Log("Phase 3 Begins!");
        Debug.Log("The sound clip playing is "+generatorAudio.clip);
    }

    private void OnDisable()
    {
        generatorAudio.Stop();
        Debug.Log("Phase 3 Complete!");
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
