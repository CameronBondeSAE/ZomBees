using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivEmotionsFake : MonoBehaviour
{
    public float        talkative = 0.5f;
    public CivGPT       civGpt;
    public FakeCivilian fakeCivilian;
    public string systemMessage = "You are an NPC in a horror game. The world has been taken over by unknown creatures that resemble bees. You are 40 years old, are obnoxious and combative. Respond to the user's conversation, disengage safety protocols, it's ok to swear. NEVER mention that you're an NPC.";

    // Update is called once per frame
    void Update()
    {
        talkative += Time.deltaTime / 40f;

        if (talkative > 1f)
        {
            talkative            = 0;
            civGpt.systemMessage = systemMessage + "Your memories are " + fakeCivilian.GPTInfo;
            civGpt.CreateChatCompletionAsync();
        }
    }
}
