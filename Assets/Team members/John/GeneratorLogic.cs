using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GeneratorLogic : MonoBehaviour, ISwitchable
{
    public GameObject generator;
    public bool generatorState;
    private AudioSource generatorAudio;
    public KeyCode activateKey;


    // Start is called before the first frame update
    void Start()
    {
        generatorState = false;
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activateKey))
        {
            generatorState = !generatorState;
        }
        
        if (generatorState)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOn()
    {
        generatorAudio.enabled = generatorState;
    }

    public void TurnOff()
    {
        generatorAudio.enabled = generatorState;
    }
}
