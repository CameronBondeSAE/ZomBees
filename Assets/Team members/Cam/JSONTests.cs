using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Personality
{
    Scared,
    Cautious,
    Leader,
    Coward,
    ReligiousNut
}

[Serializable]
public class Conversation
{
    public List<CivilianStats> conversationList;

    public Conversation()
    {
        conversationList = new List<CivilianStats>();
    }
}

[Serializable]
public class CivilianStats
{
    public string characterName;

    public Personality personality;
    // Personality   traits
    // Current       emotion
    public string outputSpeech;
    public float  speechVolume;
}

public class JSONTests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CivilianStats civilianStats = new CivilianStats
                                      {
                                          characterName = "Cam",
                                          personality   = Personality.Scared,
                                          outputSpeech  = "Hi",
                                          speechVolume  = 0.5f
                                      };

        CivilianStats civilianStats2 = new CivilianStats
                                      {
                                          characterName = "Guy",
                                          personality   = Personality.Cautious,
                                          outputSpeech  = "You suck",
                                          speechVolume  = 0.5f
                                      };

        Conversation conversation = new Conversation();
        conversation.conversationList.Add(civilianStats);
        conversation.conversationList.Add(civilianStats2);


        string json = JsonUtility.ToJson(conversation, true);
        Debug.Log(json);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
