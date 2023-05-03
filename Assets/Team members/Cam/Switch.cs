using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Switch : MonoBehaviour, ISwitchable
{
    public bool  state;

    public delegate void SwitchDelegate(object sender, EventArgs args);

    public event SwitchDelegate SwitchEvent;

    

    [Button]
    public void TurnOn()
    {
        SwitchEvent?.Invoke(this, EventArgs.Empty);
    }
    public void TurnOff()
    {
    }

    public void Toggle()
    {
        throw new NotImplementedException();
    }
}
