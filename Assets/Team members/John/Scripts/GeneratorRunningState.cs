using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class GeneratorRunningState : MonoBehaviour, ISwitchable
{
    public AudioClip generatorRunning;
    private AudioSource generatorAudio;
    public bool isEnabled;
    public float audioClipTimer;

    private void OnEnable()
    {
        isEnabled = true;
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorRunning;
        StartCoroutine(PlaySoundLoop());
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
    }
    
    [Button]
    private void OnDisable()
    {
        isEnabled = false;
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorShuttingDownState>());
    }

    public void TurnOn()
    {
        throw new NotImplementedException();
    }

    public void TurnOff()
    {
        OnDisable();
    }
    
    IEnumerator PlaySoundLoop()
    {
        while (true)
        {
            if (isEnabled)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                yield return new WaitForSeconds(audioClipTimer);
            }
            else
            {
                GetComponent<AudioSource>().Stop(); 
                yield return null;
            }
        }
    }
    
    
    public void PlaySound()
    {
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorRunning;
        generatorAudio.Play();
    }
}
