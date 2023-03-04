using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLogic : MonoBehaviour
{
    void OnEnable()
    {
        RefferenceScript.theLightIsSwitchedOn  += TurnOnTheLight;
    }
    
    void OnDisable()
    {
        RefferenceScript.theLightIsSwitchedOn  -= TurnOnTheLight;
    }

    void TurnOnTheLight()
    {
        Debug.Log("The Light Is Now On");
    }
}
