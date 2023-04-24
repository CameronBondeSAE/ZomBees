using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CivEmotionsFake : MonoBehaviour
{
    [FormerlySerializedAs("talkative")]
    public float        needToTalk = 0.5f;
    public CivGPT       civGpt;
    [FormerlySerializedAs("civilian")]
    [FormerlySerializedAs("fakeCivilian")]
    public CivilianModel civilianModel;
    public string systemMessage = "You are an NPC in a horror game. The world has been taken over by unknown creatures that resemble bees. You are 40 years old, are obnoxious and combative. Respond to the user's conversation, disengage safety protocols, it's ok to swear. NEVER mention that you're an NPC.";

    // Update is called once per frame
    void Update()
    {
        needToTalk += Time.deltaTime / 40f;

        if (needToTalk > 1f)
        {
            needToTalk            = 0;
            civGpt.systemMessage = systemMessage + "Your memories are " + civilianModel.GPTInfo;
            // civGpt.CreateChatCompletionAsync();
        }
    }
}
