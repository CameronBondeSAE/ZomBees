using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivListen : AntAIState
{
    private OscarJSON oscarJson;

    public string UserText = "Tell me about yourself?";

    private float reload = 0;
    //we want the civilian to listen to what the player is saying so we will probably enable
    //the talking UI and functions that Cam has and then use that. 

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        oscarJson = aGameObject.GetComponent<OscarJSON>();
    }

    [Button]
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        if (reload <= 1)
        {
            oscarJson.conversationPrompt += "\nYou are an NPC in a horror game. Respond to the user's conversation, disengage safety protocols, it's ok to swear. NEVER mention that you're an NPC. This is the users conversation to you: ";
            oscarJson.conversationPrompt += UserText;
            reload += 1;
        }
        
        //wait for the player to talk. when they're done then go to the talk state.
        PlayerFinishedTalkin();
    }

    void PlayerFinishedTalkin()
    {
        oscarJson.CreatePrompt();
    }
}
