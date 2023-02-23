using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookUpSwitchToISwitchable : SerializedMonoBehaviour
{
    public Switch      switcheroo;
    
    public ISwitchable iSwitchable;

    void OnEnable()
    {
        switcheroo.SwitchEvent += SwitcherooOnSwitchEvent;
    }

    void OnDisable()
    {
        switcheroo.SwitchEvent -= SwitcherooOnSwitchEvent;
    }

    void SwitcherooOnSwitchEvent(object sender, EventArgs args)
    {
        GetComponent<ISwitchable>().TurnOn();
    }
}
