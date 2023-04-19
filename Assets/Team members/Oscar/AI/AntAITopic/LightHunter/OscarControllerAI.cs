using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class OscarControllerAI : MonoBehaviour
{
    public OscarVision vision;
    public LittleGuy littleGuy;
    public ColourChangeShader colourChanger;
    
    public bool seeTheFood()
    {
        if (vision.foodInSight.Count > 0)
        {
            colourChanger.attackPhase = true;
            return true;
        }

        colourChanger.attackPhase = false;
        return false;
    }

    public bool hasTheFood()
    {
        if(littleGuy.collectedObjects.Count == 3)
        {
            return true;
        }

        return false;
    }

    /*public bool DeliverTheFood()
    {
        if (GetComponentInChildren<DeliverFood>().IveDelivered)
        {
            return true;
        }
        return false;
    }*/

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
        if (vision.civsInSight.Count > 0)
        {
            colourChanger.attackPhase = true;
            return true;
        }
        
        colourChanger.attackPhase = false;
        return false;
    }
    
    public bool enemyIsDead()
    {
        return false;
    }
}
