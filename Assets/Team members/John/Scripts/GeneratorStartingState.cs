using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;
public class GeneratorStartingState : MonoBehaviour
{
    public AudioClip generatorStartUp;
    private AudioSource generatorAudio;
    private bool componentActive = true;
    public StateManager stateMachine;

    public void OnEnable()
    {
        stateMachine.ChangeState(this);
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorStartUp;
        generatorAudio.Play();
        Debug.Log("Phase 1 Begins!");
        Debug.Log("The sound clip playing is "+generatorAudio.clip);
    }

    public void OnDisable()
    {
        Debug.Log("Phase 1 complete!");
        generatorAudio.Stop();
    }
    
    // new public method to toggle the enabled state of the component
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
