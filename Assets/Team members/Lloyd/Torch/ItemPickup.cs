using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ItemPickup : IPickup
{
    public bool held = false;
    public bool active = false;

    public bool useable = true;

    public enum PickupState
    {
        Idle,
        Active,
        Held,
        HeldActive,
        Destroyed
    }

    public PickupState myState;

    public void SwitchHeld(GameObject parent)
    {
        held = !held;
        
        if (held)
        {
            gameObject.transform.SetParent(null);
            held = false;

            if (!active)
                ChangeState(PickupState.Idle);
            else
            {
                ChangeState(PickupState.Active);
            }
        }
        
        if (!held)
        {
            gameObject.transform.SetParent(parent.transform);
            gameObject.transform.position = parent.transform.position;
            gameObject.transform.rotation = parent.transform.rotation;
            held = true;
            ChangeState(PickupState.Held);
        }
    }
    
    public void SwitchActive()
    {
        if (useable)
        {
            active = !active;

            if (active && !held)
                ChangeState(PickupState.Active);

            else if (active && held)
                ChangeState(PickupState.HeldActive);

            else if (!active && held)
                ChangeState(PickupState.Held);

            else
                ChangeState(PickupState.Idle);
        }
    }

    [Button("Change State")]
    public void ChangeState(PickupState newState)
    {
        PickupState prevState = myState;
        if (newState == prevState)
            return;

        myState = newState;
        OnAnnouncePickupStatus(myState);
    }

    public void Destroyed()
    {
        useable = false;
        ChangeState(PickupState.Destroyed);
    }

    public event Action<PickupState> AnnouncePickupStatus;

    public void OnAnnouncePickupStatus(PickupState announceState)
    {
        AnnouncePickupStatus?.Invoke(announceState);
    }
}