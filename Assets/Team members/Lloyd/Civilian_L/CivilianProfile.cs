using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianProfile : MonoBehaviour
{
    public CivEventArgs myArgs;
    public CivEventArgs.Team myTeam;
    public string myName;
    public float beeness;
    public float hunger;
    public float fear;
    public  float speechVolume;
    public  float loyaltyToPlayer;
    public CivEventArgs.ActionState myActionState;
    public CivEventArgs.Emotions myEmotion;
    public CivEventArgs.CivCiv myCivCiv;
    public CivEventArgs.Topic myTopic;
    public CivEventArgs.Personality myPersonality;
    public CivEventArgs.Character myCharType;

    Vector3 target;

    public void SetStats(CivEventArgs args)
    {
        myArgs = args;
        myTeam = args.MyTeam;
        myName = args.CivName;
        beeness = args.beeness;
        hunger = args.hunger;
        fear = args.fear;
        loyaltyToPlayer = args.loyaltyToPlayer;
        myEmotion = args.MyEmotion;
        myCivCiv = args.MyCivCiv;
        myPersonality = args.MyPersonality;
        myCharType = args.MyCharacter;
    }

    public void StartGame()
    {
        TileCivMouth mouth = GetComponent<TileCivMouth>();
        mouth.StartGame(myArgs);
    }
}