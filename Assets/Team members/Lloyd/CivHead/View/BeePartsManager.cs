using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;

public class BeePartsManager : MonoBehaviour
{
    public GameObject mandibles;
    public GameObject antannae;
    public GameObject beeEyes;

    public GameObject humanEyes;

    [Button]
    public void BeeEyes()
    {
        humanEyes.SetActive(false);
        beeEyes.SetActive(true);
    }

    [Button]
    public void HumanEyes()
    {
        humanEyes.SetActive(true);
        beeEyes.SetActive(false);
    }

    [Button]
    public void GrowAntannae()
    {
        antannae.SetActive(true);
    }

    [Button]
    public void LoseAntannae()
    {
        antannae.SetActive(false);
    }

    [Button]
    public void GrowMandibles()
    {
        mandibles.SetActive(true);
    }

    [Button]
    public void LoseMandibles()
    {
        mandibles.SetActive(false);
    }
}
