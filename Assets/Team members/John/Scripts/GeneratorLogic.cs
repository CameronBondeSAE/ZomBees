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

    public delegate void PoweredOn();
    //Change this event to something like ZoneOneHasBeenPoweredOn if we will
    //have a set amount of powered objects tied to this generator and other powered
    //objects tied to other generators
    public static event PoweredOn TheGeneratorHasBeenTurnedOn;


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
        if (TheGeneratorHasBeenTurnedOn != null)
        {
            TheGeneratorHasBeenTurnedOn();
        }
    }

    public void TurnOff()
    {
        generatorAudio.enabled = generatorState;
    }
}
