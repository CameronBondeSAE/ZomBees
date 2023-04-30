using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using Oscar;
using UnityEngine;
using Virginia;

public class OscarControllerAI : MonoBehaviour
{
    public OscarVision vision;
    public LittleGuy littleGuy;
    public ColourChangeShader colourChanger;
    public Inventory inventory;

    public GameObject basicBeeWalking;
    public GameObject basicBeeFlying;

    private void Awake()
    {
        //GetComponent<BeeWingsManager>().SetWings();
        basicBeeFlying.SetActive(false);
        basicBeeWalking.SetActive(true);
    }

    public bool seeTheFood()
    {
        if (vision.foodInSight.Count > 0)
        {
            //colourChanger.attackPhase = true;
            return true;
        }

        //colourChanger.attackPhase = false;
        return false;
    }

    public bool hasTheFood()
    {
        if(inventory.heldItem != null)
        {
            return true;
        }

        return false;
    }

    public bool DeliverTheFood()
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
        if (vision.civsInSight.Count > 0)
        {
            //colourChanger.attackPhase = true;
            return true;
        }
        
        //colourChanger.attackPhase = false;
        return false;
    }
    
    public bool enemyIsDead()
    {
        return false;
    }
}
