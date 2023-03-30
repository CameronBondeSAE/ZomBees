using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class OscarCivController : MonoBehaviour
{
    public OscarCivVision vision;
    public LittleGuy littleGuy;

    public bool SeeBeeBool()
    {
        return vision.beesInSight.Count >= 1;
    }

    public bool IsScaredBool()
    {
        return false;
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
        return false;
    }
}
