using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private void OnEnable()
    {
        GeneratorLogic.TheGeneratorHasBeenTurnedOn += TheGeneratorIsOn;
    }

    private void OnDisable()
    {
        GeneratorLogic.TheGeneratorHasBeenTurnedOn += TheGeneratorIsOn;
    }

    public void TheGeneratorIsOn()
    {
        Debug.Log("The Generator Is On");
    }

}
