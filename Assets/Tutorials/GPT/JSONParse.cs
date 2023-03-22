using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// Fake test classes only
class PromptResults
{
	public enum Actions
	{
		FollowPlayer,
		RunAway
	}

	public string characterName;
	public string personality;
	public string outputSpeech;
	public bool announcement;
	public Actions action;
}

class Emotions
{
	public float fear;
	// MORE
}

public class WorldStateConditions
{
	public float clock;
	public bool homeBaseInvaded = true;
	public bool canSeeBee = true;
	public bool isHiding = true;
}


//
// Testing JSON and prompt creation
//
public class JSONParse : MonoBehaviour
{
	[TextArea(5, 40)]
	public string finalPrompt;

	[Button]
	void CreatePrompt()
	{
		// Generate fake objects to test
		WorldStateConditions worldStateConditions = new WorldStateConditions
		{
			clock = 0300,
			homeBaseInvaded = true,
			canSeeBee = true,
			isHiding = false
		};
		Emotions emotions = new Emotions
		{
			fear = 1f
		};
		
		// Insert conditions into prompt
		// Note the @ syntax allows special characters to included. In this case quotes. If you put a double "" it'll count as one.
		// TEST TEST TEST TEST I've no idea what I'm doing, so use this to experiment as it works to some degree.

		// Setup for GPT to get an idea
		// TODO: Add actions dynamically from an master enum or something?
		// TODO: Do we need personality or should this set their emotions (or shift their emotions)
		finalPrompt = @"Using this JSON as a template. Fear is a 0 to 1 float value. Replace ""ExamplePersonality"" with either Angry, Scared, Happy
		Replace ""ExampleAction"" with either FollowPlayer, RunAway, FrozenWithFear
		Announcement is a bool and is true if they aren’t replying to a specific character
		";
		finalPrompt += "";
		// Example JSON to teach GPT what to return
		finalPrompt += @"
		{
				""characterName"": ""NameGoesHere"",
				""personality"": ExamplePersonality,
				""outputSpeech"": ""Example"",
				""announcement"": true,
				""action"": ExampleAction
		}";

		// Injecting live conditions about the CIV and the WORLD for GPT to make better chat and action decisions
		finalPrompt += "\nThe 24 hour time = " + worldStateConditions.clock;
		finalPrompt += "\nisHiding = " + worldStateConditions.isHiding;
		finalPrompt += "\ncanSeeBee = " + worldStateConditions.canSeeBee;
		finalPrompt += "\nhomeBaseInvaded = " + worldStateConditions.homeBaseInvaded;
		
		finalPrompt += "\nfear = " + emotions.fear;

		// Final instruction for GPT
		finalPrompt += "\nUse this information to generate a spontaneous outburst from the character";
		
		Debug.Log(finalPrompt);
	}

	[Button]
	void CreateJSON()
	{
		PromptResults promptResults = new PromptResults();

		// Test creating c# class from JSON
		string json = JsonUtility.ToJson(promptResults, true);
		Debug.Log(json);
	}

	[Button]
	void ReadJSON()
	{
		PromptResults promptResults = new PromptResults();

		promptResults = JsonUtility.FromJson<PromptResults>(finalPrompt);

		Debug.Log(promptResults.characterName);
		Debug.Log(promptResults.personality);
		Debug.Log(promptResults.outputSpeech);
		Debug.Log(promptResults.announcement);
		Debug.Log(promptResults.action);
	}

}


