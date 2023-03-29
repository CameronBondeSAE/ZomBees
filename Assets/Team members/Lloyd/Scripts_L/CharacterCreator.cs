using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

public class CharacterCreator : MonoBehaviour
{
    //written along ChatGPT

    private Random rand;

    public CivEventArgs playerArgs;

    public CivEventArgs.Team myTeam;

    GameObject source;

    //safe room, food, fight, Player, etc...
    Vector3 target;

    public string enteredName;
    string myName;

    float loyaltyToPlayer;
    float beeness;
    float hunger;
    float fear;
    float speechVolume;

    CivEventArgs.ActionState myActionState;
    CivEventArgs.Emotions myEmotion;
    CivEventArgs.CivCiv myCivCiv;
    CivEventArgs.Topic myTopic;
    CivEventArgs.Character myCharacter;
    CivEventArgs.Personality myPersonality;

    private bool set = false;

    private float randomFloat;

    private int address;

    [Button]
    public void RandomiseOfType(CivEventArgs.Team team)
    {
        if (team == CivEventArgs.Team.Player)
            RandomisePlayer();

       // else if (team == CivEventArgs.Team.Human)
         //   RandomiseCiv();

        else if (team == CivEventArgs.Team.Bee)
            RandomiseZombee();
    }

    public float RandomFloat()
    {
        rand = new Random();
        randomFloat = (float)rand.NextDouble();
        return randomFloat;
    }

    #region Player

    public void RandomisePlayer()
    {
        myTeam = CivEventArgs.Team.Player;
        myCharacter = CivEventArgs.Character.Player;
        if (enteredName == "Random")
        {
            RandomNameGenerator nameGenerator = new RandomNameGenerator();
            string randomName = nameGenerator.GetRandomName(myPersonality);
            myName = randomName;
        }
        else myName = enteredName;

        loyaltyToPlayer = 100;
        beeness = 0;

        target = Vector3.zero;
        hunger = 0;
        fear = 0;
        speechVolume = 0.5f;

        myActionState = CivEventArgs.ActionState.WaitingToSpawn;

        if (myEmotion == CivEventArgs.Emotions.Random)
        {
            Random random = new Random();
            address = random.Next(1, Enum.GetNames(typeof(CivEventArgs.Emotions)).Length);
            myEmotion |= (CivEventArgs.Emotions)(1 << (address - 1));
        }

        if (myCivCiv == CivEventArgs.CivCiv.Random)
        {
            Random random = new Random();
            address = random.Next(1, Enum.GetNames(typeof(CivEventArgs.CivCiv)).Length);
            myCivCiv |= (CivEventArgs.CivCiv)(1 << (address - 1));
        }

        myTopic = CivEventArgs.Topic.Silent;

        if (myPersonality == CivEventArgs.Personality.Random)
        {
            Random random = new Random();
            address = random.Next(1, Enum.GetNames(typeof(CivEventArgs.Personality)).Length);
            myPersonality |= (CivEventArgs.Personality)(1 << (address - 1));
        }

        myCharacter = CivEventArgs.Character.Player;

        playerArgs = new CivEventArgs(gameObject, target, myTeam, myName, loyaltyToPlayer, beeness, hunger, fear,
            speechVolume, myActionState, myEmotion, myTopic, myCharacter, myCivCiv, myPersonality);
        Debug.Log(gameObject + " , " + target + " , " + myTeam + " , " + myName + " , " + loyaltyToPlayer + " , " +
                  beeness + " , " + hunger + " , " + fear + " , " + speechVolume + " , " + myActionState + " , " +
                  myEmotion + " , " + myTopic + " , " + myCharacter + " , " + myCivCiv + " , " + myPersonality);
    }

    #endregion

    #region Civ

    public void RandomiseCiv(GameObject civObj)
    {
        myTeam = CivEventArgs.Team.Human;

        myCharacter = CivEventArgs.Character.NPC;

        RandomNameGenerator nameGenerator = new RandomNameGenerator();
        string randomName = nameGenerator.GetRandomName(myPersonality);
        myName = randomName;

        rand = new Random();
        randomFloat = (float)rand.NextDouble();
        loyaltyToPlayer = randomFloat;


        beeness = RandomFloat();
        hunger = RandomFloat();
        fear = RandomFloat();
        speechVolume = 0.5f;

        myActionState = CivEventArgs.ActionState.WaitingToSpawn;

        rand = new Random();
        address = rand.Next(1, Enum.GetNames(typeof(CivEventArgs.Emotions)).Length);
        myEmotion |= (CivEventArgs.Emotions)(1 << (address - 1));

        rand = new Random();
        address = rand.Next(1, Enum.GetNames(typeof(CivEventArgs.CivCiv)).Length);
        myCivCiv |= (CivEventArgs.CivCiv)(1 << (address - 1));

        myTopic = CivEventArgs.Topic.Silent;

        Random random = new Random();
        address = random.Next(1, Enum.GetNames(typeof(CivEventArgs.Personality)).Length);
        myPersonality |= (CivEventArgs.Personality)(1 << (address - 1));


        target = Vector3.zero;

        playerArgs = new CivEventArgs(civObj, target, myTeam, myName, loyaltyToPlayer, beeness, hunger, fear,
            speechVolume, myActionState, myEmotion, myTopic, myCharacter, myCivCiv, myPersonality);
        Debug.Log("NAME : "+myName);
        Debug.Log("CIV: "+myCivCiv);
        Debug.Log("PERSONALITY : "+myPersonality);
        Debug.Log("CHARACTER : "+myCharacter);
        Debug.Log("LOYAL: "+loyaltyToPlayer);
        Debug.Log("BEENESS : "+beeness);
        Debug.Log("FEAR : "+fear);
        Debug.Log("VOLUME : "+speechVolume);
        Debug.Log("ACTION : "+myActionState);
        Debug.Log("EMOTION : "+myEmotion);
    }

    #endregion

    #region Zombee

    public void RandomiseZombee()
    {
    }

    #endregion
}