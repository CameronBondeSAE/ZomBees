using System.Collections;
using System.Collections.Generic;
using Marcus;
using Sirenix.OdinInspector;
using UnityEngine;

// Fake test classes only
public class LloydPromptResults
{
    public CivEventArgs myArgs;
    public CivEventArgs.Team myTeam;
    public string myName;
    public float beeness;
    public float hunger;
    public float fear;
    public float speechVolume;
    public float loyaltyToPlayer;
    public CivEventArgs.ActionState myActionState;
    public CivEventArgs.Emotions myEmotion;
    public CivEventArgs.CivCiv myCivCiv;
    public CivEventArgs.Topic myTopic;
    public CivEventArgs.Personality myPersonality;
    public CivEventArgs.Character myCharType;

    public bool announcement;
}

//
// Testing JSON and prompt creation
//
public class LloydJSONParse : MonoBehaviour
{
    LloydPromptResults promptResults;
    
    [TextArea(5, 40)] public string finalPrompt;

    [Button]
    public void CreatePrompt(CivEventArgs args)
    {
        // Insert conditions into prompt
        // Note the @ syntax allows special characters to included. In this case quotes. If you put a double "" it'll count as one.
        // TEST TEST TEST TEST I've no idea what I'm doing, so use this to experiment as it works to some degree.
        
        promptResults = new LloydPromptResults()
        {
            myArgs = args,
            myTeam = args.MyTeam,
            myName = args.CivName,
            beeness = args.beeness,
            hunger = args.hunger,
            fear = args.fear,
            loyaltyToPlayer = args.loyaltyToPlayer,
            speechVolume = args.speechVolume,
            myActionState = args.MyActionState,
            myTopic = args.MyTopic,
            myEmotion = args.MyEmotion,
            myPersonality = args.MyPersonality,
            myCharType = args.MyCharacter,
            myCivCiv = args.MyCivCiv,

            announcement = true,
        };

        // Setup for GPT to get an idea
        // TODO: Add actions dynamically from an master enum or something?
        // TODO: Do we need personality or should this set their emotions (or shift their emotions)
        finalPrompt =
            @"Using this JSON as a template. All floats are a 0 to 1 float value, with 0 being min and 1 being max. Beeness determines how much of a bee you are (0 being human, 1 being bee). Replace the corresponding example values with the input values.
		Announcement is a bool and is true if they aren’t replying to a specific character. Reply in the voice of the character, inflected with emotion and personality and the country of origin
		";
        finalPrompt += "";
        // Example JSON to teach GPT what to return
        finalPrompt += @"
		{
				""myName"": NameGoes,
                ""float"": ExampleFloat,
				""personality"": ExamplePersonality,
                ""emotion"": ExampleEmotion,
                ""character"": ExampleCharacter,
                ""Personality"": ExamplePersonality,
				""outputSpeech"": ""Example"",
				""announcement"": true,
				""action"": ExampleAction
        }";

        finalPrompt += "\nBeeness =" +args.beeness;
        finalPrompt += "\nFear =" +args.fear;
        finalPrompt += "\nFearLevel =" +args.hunger;
        
        finalPrompt += "\nEmotion =" +args.MyEmotion;
        finalPrompt += "\nCharacter =" + args.MyCharacter;
        finalPrompt += "\nPersonality =" + args.MyPersonality;

        // Final instruction for GPT
        finalPrompt += "\nUse this information to generate a spontaneous outburst from the character";

        Debug.Log(finalPrompt);

        string thing;
        int var1 = 99;
        thing = $@"Cam is ""cool"" because {var1}";
        CreateJSON(promptResults);
    }

    [Button]
    public void CreateJSON(LloydPromptResults promptResults)
    {
      // Test creating c# class from JSON
        string json = JsonUtility.ToJson(promptResults, true);
        Debug.Log(json);
    }

    [Button]
    public void ReadJSON()
    {
        promptResults = JsonUtility.FromJson<LloydPromptResults>(finalPrompt);

        Debug.Log(promptResults.myName);
        Debug.Log(promptResults.myPersonality);
        Debug.Log(promptResults.beeness);
        Debug.Log(promptResults.hunger);
        Debug.Log(promptResults.fear);
        Debug.Log(promptResults.loyaltyToPlayer);
        Debug.Log(promptResults.speechVolume);
        Debug.Log(promptResults.announcement);
        Debug.Log(promptResults.myEmotion);
        Debug.Log(promptResults.myCivCiv);
        Debug.Log(promptResults.myTopic);
        Debug.Log(promptResults.myPersonality);
        Debug.Log(promptResults.myCharType);
        Debug.Log(promptResults.speechVolume);
    }
}