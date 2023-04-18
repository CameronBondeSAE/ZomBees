using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class EggManager : MonoBehaviour
{
    public GameObject eggObjectPrefab;

    public AlienEggPulse eggLogic;

    public GameObject originalCiv;

    public GameObject zombeeCiv;

    public List<Vector3> eggList;
    
    public static EggManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Button]
    public void StartEgg(GameObject newOriginalCiv)
    {
        originalCiv = newOriginalCiv;
        DisableObject(originalCiv);

        GameObject instantiateEgg = Instantiate(eggObjectPrefab, originalCiv.transform.position, Quaternion.identity);

        eggLogic = instantiateEgg.GetComponent<AlienEggPulse>();

        SubscribeToEggEvents();
    }
    
    private void SubscribeToEggEvents()
    {
            eggLogic.SafeEvent += FreeCiv;
            eggLogic.TimesUpEvent += SpawnBee;
    }

    public void DisableObject(GameObject gameObjectToDisable)
    {
        gameObjectToDisable.SetActive(false);
    }

    public void FreeCiv()
    {
        if (originalCiv != null)
        {
            originalCiv.SetActive(true);
            originalCiv = null;
        }
    }

    public void SpawnBee()
    {
        Instantiate(zombeeCiv, originalCiv.transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        eggLogic.SafeEvent -= FreeCiv;
        eggLogic.TimesUpEvent -= SpawnBee;
    }
}