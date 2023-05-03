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
    public Hearing ears;

    public GameObject basicBeeWalking;
    public GameObject basicBeeFlying;

    private bool retreat;
    private bool iSeeFood;
    private bool iHaveFood;
    private bool iSeeLight;
    private bool iSeeCivs;
    
    private void Awake()
    {
        //GetComponent<BeeWingsManager>().SetWings();
        basicBeeFlying.SetActive(false);
        basicBeeWalking.SetActive(true);
        ears.SoundHeardEvent += HeardSound;
    }

    private void HeardSound(SoundProperties sounds)
    {
        if (sounds.SoundType == SoundEmitter.SoundType.CreatureRepellant)
        {
            print("ears = "+ears.loudestRecentSound.SoundType);
            print(sounds.SoundType);
            RunAway = true;
        }
    }

    private void FixedUpdate()
    {
        if (vision.foodInSight.Count > 0)
        {
            seeTheFood = true;
            //colourChanger.attackPhase = true;
        }
        else
        {
            seeTheFood = false;
        }
        
        if (vision.lightInSight.Count > 0)
        {
            seeTheLight = true;
        }
        else
        {
            seeTheLight = false;
        }
        
        if (vision.civsInSight.Count > 0)
        {
            seeCivilians = true;
        }
        else
        {
            seeCivilians = false;
        }
        

        if(inventory.heldItem != null)
        {
            hasTheFood = true;
        }
        else
        {
            hasTheFood = false;
        }
    }

    public bool seeTheFood
    {
        get { return iSeeFood; }
        set { iSeeFood = value; }
    }

    public bool hasTheFood
    {
        get { return iHaveFood; }
        set { iHaveFood = value; }
    }

    public bool DeliverTheFood()
    {
        return false;
    }

    public bool seeTheLight
    {
        get { return iSeeLight; }
        set { iSeeLight = value; }
    }

    public bool seeCivilians
    {
        get { return iSeeCivs; }
        set { iSeeCivs = value; }
    }
    
    public bool enemyIsDead()
    {
        return false;
    }

    public bool RunAway
    {
        get { return retreat; }
        set { retreat = value; }
    }
}
