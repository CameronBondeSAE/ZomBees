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

    [Button]
    private void StartEgg()
    {
        DisableObject(originalCiv);

        GameObject instantiatEgg = Instantiate(eggObjectPrefab, transform.position, Quaternion.identity);

        eggLogic = instantiatEgg.GetComponent<AlienEggPulse>();

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
        if (originalCiv != null)
        {
            Destroy(originalCiv);
            Destroy(eggObjectPrefab);
        }

        Instantiate(zombeeCiv, transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        eggLogic.SafeEvent -= FreeCiv;
        eggLogic.TimesUpEvent -= SpawnBee;
    }
}