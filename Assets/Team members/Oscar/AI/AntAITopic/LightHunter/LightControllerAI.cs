using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class LightControllerAI : MonoBehaviour
{
    public LightVisionAI vision;

    public bool seeTheHoney()
    {
        if (vision.honeyInSight.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool hasTheHoney()
    {
        return false;
    }

    public bool seeTheLight()
    {
        if (vision.lightInSight.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    
    public bool enemyIsDead()
    {
        return false;
    }
}
