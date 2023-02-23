using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookUpSwitchToLight : MonoBehaviour
{
    public Switch      switcheroo;
    public Cam.LightEnable lightEnable;

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
        // lightEnable.CamSwitchOnSwitchEvent();
    }
}
