using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class OscarControllerAI : MonoBehaviour
{
    public OscarVisionAI vision;
    
    public bool seeTheFood()
    {
        if (vision.honeyInSight.Count > 0)
        {
            return true;
        }

        return false;
    }

    public bool hasTheFood()
    {
        return false;
    }

    public bool seeTheLight()
    {
        if (vision.lightInSight.Count > 0)
        {
            return true;
        }
        return false;
    }

    public bool seeCivilians()
    {
        if (vision.civiliansVisible.Count > 0)
        {
            return true;
        }

        return false;
    }
    
    public bool enemyIsDead()
    {
        return false;
    }
}
