using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorShuttingDownState : MonoBehaviour
{
    
    public AudioClip generatorShuttingDown;

    private AudioSource generatorAudio;

    private void OnEnable()
    {
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
}
