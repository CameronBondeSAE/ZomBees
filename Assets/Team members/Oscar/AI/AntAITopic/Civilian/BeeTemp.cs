using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeTemp : MonoBehaviour
{
    #region Wings

    private BeeWingsManager beeWings;

    private void OnEnable()
    {
        StartWings();
    }

    private void StartWings()
    {   
        // beeWings = GetComponentInChildren<BeeWingsManager>();
        // beeWings.SetWings();
    }

    //change wing angles (eg depending on state)
    //angle must be entered negatively. default is -90
    //speed is how fast... it... flaps...
    //alive must be set to true, unless you want wings to stop flapping
    public void ChangeWings(float angle, float speed, bool alive)
    {
        beeWings.OnChangeStatEvent(angle, speed, alive);
    }

    #endregion
}
