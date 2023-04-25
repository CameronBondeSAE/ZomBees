using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;
using Lloyd;

public class OscarBruteCivController : MonoBehaviour
{
    public OscarVision vision;
    public LittleGuy littleGuy;
    public Hearing ears;
    private bool hearSounds;
    public bool playerTalkin;

    private void OnEnable()
    {
        // ears.SoundHeardEvent += CreateFear;
    }

    private void OnDisable()
    {
        // ears.SoundHeardEvent -= DecreaseFear;
    }

    public bool SeeBeeBool()
    {
        return vision.beesInSight.Count >= 1;
    }

    // void CreateFear(HearingEventArgs eventArgs)
    // {
    //     hearSounds = true;
    // }
    //
    // void DecreaseFear(HearingEventArgs eventArgs)
    // {
    //     hearSounds = false;
    // }
    
    public bool IsScaredBool()
    {
        if (ears.heardSound)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool StayAliveBool()
    {
        return false;
    }

    public bool KilledBeeBool()
    {
        return false;
    }

    public bool PlayerIsTalking()
    {
        return playerTalkin;
    }

    public bool PrioritiseThePlayer()
    {
        return false;
    }
}
