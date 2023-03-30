using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivEventArgs : EventArgs
{
    // written with chat GPT
    #region Variables

    //
    public GameObject Source { get; set; }

    //targeting location
    public Vector3 TargetVector3 { get; set; }

    //strings
    public string CivName { get; set; }

    //floats

    public float loyaltyToPlayer { get; set; }

    public float beeness { get; set; }

    public float hunger { get; set; }

    public float fear { get; set; }

    public float speechVolume { get; set; }
    
    #endregion

    #region Enums

    // Physical Action States

    public ActionState MyActionState { get; set; }

    public enum ActionState
    {
        Idle,
        // hide in saferoom / cower
        StayStill,

        Talk,
        Listen,

        WalkTo,
        RunTo,
        
        FollowingPlayer,

        // gather resources, press button, etc 
        Interact,
        
        WaitingToSpawn
    }

    // Character Type
    public Character MyCharacter { get; set; }

    public enum Character
    {
        Player,
        
        QuestNPC,
        
        NPC,
        
        ZombeeNPC
    }

    // Country of Origin
    public CivCiv MyCivCiv { get; set; }

  [Flags]  public enum CivCiv
    {
        Random=1,
        Australian=2,
        AustralianBogan=4,

        French=8,
        OverlyBritish=16,
        Welsh=32,
        
        Greek=64,
        Czechoslovakian=128,
        Croatian = 256,
        
        Scottish,
        Irish,
        Dutch,
        Italian,
        Russian
    }

    // Emotional states, effects conversations, stats, actions, etc

    public Emotions MyEmotion { get; set; }

   [Flags] public enum Emotions
    {
        Random=1,
        
        Hungry=2,

        Calm=4,
        Hardened=8,

        Paranoid=16,
        Schizophrenic=32,
        Panic=64,

        Pious=128,

        Rambo=256,
        
        Obnoxious,
        
        Combative,
        
        Sarcastic,
        
        Contrarian,
        
        Introverted,
        
        Traumatised,
        
        UsesProfanity
    }
   
   // determines character emotional profile
   public Personality MyPersonality { get; set; }
[Flags]
   public enum Personality
   {
       Random=1,
       HotHead=2,
       
       ScaredyCat=4,
       
       ProneToBees=8,
       
       ForTheHumans=16,
       
       Celebrity=32,
       
       Childish=64,
       
       Beelot=128,
       HalfBee=256,
       RecoveredBee=512,

       Scientist=1024,
       Military=2048,

       Child=4096,
       Elderly=8192
   }

    // Allegience, Neutral / Human / Bee
    //
    // Human assumes full Player loyalty? add a fourth?
    public Team MyTeam { get; set; }

    public enum Team
    {
        Neutral,
        Player,
        Human,
        Bee
    }

    // Topic of Conversation
    public Topic MyTopic { get; set; }

    public enum Topic
    {
        Silent,
        
        Quest,
        
        Memories,

        Zombees,

        Story,

        //weather
        WorldState,

        CivType,
        CivCiv,

        Emotions
    }
    
    #endregion

    #region SetAll
    
    // boomshakalaka

    public CivEventArgs(GameObject source, Vector3 target, Team myTeam, string civName, float loyaltyToPlayer,
        float beeness, float hunger, float fear, float speechVolume, ActionState myActionState, Emotions myEmotion,
        Topic myTopic, Character myCharType, CivCiv myCivCiv, Personality myPersonality)
    {
        Source = source;
        MyTeam = myTeam;
        CivName = civName;
        this.beeness = beeness;
        this.hunger = hunger;
        this.fear = fear;
        this.speechVolume = speechVolume;
        this.loyaltyToPlayer = loyaltyToPlayer;
        MyActionState = myActionState;
        MyEmotion = myEmotion;
        MyCivCiv = myCivCiv;
        MyTopic = myTopic;
        MyPersonality = myPersonality;
        MyCharacter = myCharType;

        TargetVector3 = target;
    }
    #endregion
}