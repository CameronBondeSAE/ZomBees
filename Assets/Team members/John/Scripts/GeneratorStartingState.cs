using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using Sirenix.OdinInspector;
using UnityEngine;
public class GeneratorStartingState : MonoBehaviour, ISwitchable
{
    public AudioClip generatorStartUp;
    private AudioSource generatorAudio;
    private bool componentActive = true;
    public float stateSwapTimer;

    [Button]
    public void OnEnable()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
        generatorAudio = GetComponent<AudioSource>();
        generatorAudio.clip = generatorStartUp;
        generatorAudio.Play();
        StartCoroutine(DelayCoroutine(stateSwapTimer));
    }

    public void OnDisable()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorRunningState>());
        generatorAudio.Stop();
    }

    IEnumerator DelayCoroutine(float amount)
    {
        Debug.Log("Coroutine Ran Succesfully");
        yield return new WaitForSeconds(amount);
        OnDisable();
    }

    public void TurnOn()
    {
        OnEnable();
    }

    public void TurnOff()
    {
        throw new NotImplementedException();
    }
}
