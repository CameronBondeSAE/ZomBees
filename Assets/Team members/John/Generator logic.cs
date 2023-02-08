using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Generatorlogic : MonoBehaviour
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
            GeneratorActivate();
        }
        else
        {
            GeneratorDeactivate();
        }
    }

    void GeneratorActivate()
    {
        generatorAudio.enabled = generatorState;
        
    }

    void GeneratorDeactivate()
    {
        generatorAudio.enabled = generatorState;
    }
}
