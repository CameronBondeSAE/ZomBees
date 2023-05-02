using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Infected : MonoBehaviour
{
    public float countdownTilHalfBee;

    public float countdownTilEgg;

    public FaceManager faceManager;

    public BeeWingsManager beeWings;

    [ReadOnly]
    public float timeElapsed;

    public BeePartsManager beeParts;

    [ReadOnly] public int beePartsCount;
    
    /*
    public void OnEnable()
    {
        StartCoroutine(Ticking());
    }*/

    [Button]
    public void StartTicking()
    {
        StartCoroutine(Ticking());
    }

    [Button]
    public void Cure()
    {
        beeParts.Cure();
        beeWings.DeleteWings();
    }

    [Button]
    public void SetWings()
    {
        beeWings.SetWings();
        beeWings.OnChangeStatEvent(-145, 3, true);
    }

    private IEnumerator Ticking()
    {
        beePartsCount = beeParts.beeParts.Count;
        timeElapsed = 0f;
        float threshold = countdownTilHalfBee / beePartsCount;
        while (timeElapsed < countdownTilHalfBee)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= threshold)
            {
                beeParts.SpawnRandomPart();
                faceManager.RandomSadEmote();
                threshold += countdownTilHalfBee / beePartsCount;
            }

            yield return null;
        }
        
        if (Random.Range(0f, 1f) < 0.5f)
        {
            beeWings.numberOfWings = 1;
        }
        else
        {
            beeWings.numberOfWings = 2;
        }

        beeParts.BeeEyes();
        SetWings();
        StartCoroutine(TimeTilEgg());
    }

    private IEnumerator TimeTilEgg()
    {
        yield return new WaitForSeconds(countdownTilEgg);
        EggManager.instance.StartEgg(gameObject);
    }
}