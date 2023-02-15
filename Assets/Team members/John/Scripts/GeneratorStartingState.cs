using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GeneratorStartingState : MonoBehaviour
{
    public AudioClip generatorStartUp;

    private AudioSource generatorAudio;
    


    public void OnEnable()
    {
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

    public void Thingthing()
    {
        
    }
}
