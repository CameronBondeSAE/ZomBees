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

    public bool RunAway
    {
        get { return retreat; }
        set { retreat = value; }
    }
}
