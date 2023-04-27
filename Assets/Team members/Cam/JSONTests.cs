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
public class MultiConversationTest
{
    public List<CivilianStats> conversationList;

    public MultiConversationTest()
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

        MultiConversationTest multiConversationTest = new MultiConversationTest();
        multiConversationTest.conversationList.Add(civilianStats);
        multiConversationTest.conversationList.Add(civilianStats2);


        string json = JsonUtility.ToJson(multiConversationTest, true);
        Debug.Log(json);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
