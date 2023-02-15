using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorOn : MonoBehaviour
{
    public AudioClip generatorStartUp;
    public AudioClip generatorRunning;
    public AudioClip generatorShuttingDown;
    
    private AudioSource generatorAudio;
    
    
    void OnEnable()
    {
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorStartUp;
        
    }

    
    void Update()
    {
        generatorAudio.clip = generatorRunning;
    }

    void OnDisable()
    {
        generatorAudio.clip = generatorShuttingDown;
    }
}
