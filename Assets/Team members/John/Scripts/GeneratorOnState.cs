using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GeneratorOn : MonoBehaviour
{
    public AudioClip generatorStartUp;
    public AudioClip generatorRunning;
    public AudioClip generatorShuttingDown;
    
    private AudioSource generatorAudio;

    [SerializeField] private float timePassed;
    
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(timePassed);

        // Code to execute after a 2 second delay
    }
    
    
    void OnEnable()
    {
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorStartUp;
        Debug.Log("The sound clip playing is "+generatorAudio.clip);
        timePassed = 3.381f;
        StartCoroutine(DelayedAction());

    }

    
    void Update()
    {
        generatorAudio.clip = generatorRunning;
        Debug.Log("The sound clip playing is "+generatorAudio.clip);
    }

    void OnDisable()
    {
        generatorAudio.clip = generatorShuttingDown;
        Debug.Log("The sound clip playing is "+generatorAudio.clip);
        timePassed = 5.973f;
        StartCoroutine(DelayedAction());
    }
}
