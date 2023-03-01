using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class LightControllerAI : MonoBehaviour
{
    public LightVisionAI vision;

    public bool seeTheHoney()
    {
        print(vision.honeyInSight.Count > 0);
        return vision.honeyInSight.Count > 0;
    }

    public bool hasTheHoney()
    {
        return false;
    }

    public bool seeTheLight()
    {
        print(vision.lightInSight.Count > 0);
        return vision.lightInSight.Count > 0;
    }
    
    public bool enemyIsDead()
    {
        return false;
    }
}
