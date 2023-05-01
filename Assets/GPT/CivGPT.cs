using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using Anthill.AI;
using Lloyd;
using Marcus;
using OpenAI;
using OpenAI.Chat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using ChatRequest = OpenAI.Chat.ChatRequest;

// using ChatRequest = OpenAI_API.Chat.ChatRequest;

public class CivGPT : MonoBehaviour, IHear
{
	public CivCountryOfOrigin randomCountry;


	[Header("Setup")]
	public string apiKeys;

	public MemoryManager memoryManager;
	public CivilianTraits civilianTraits;
	public QuestTrackerSimple questTrackerSimple;
	public AntAIAgent antAIAgent;
	public ChatEmitter chatEmitter;


	[Header("Test area")]
	[TextArea(5, 40)]
	public string systemMessage;

	[TextArea(5, 40)]
	public string prompt;


	// Conversation currentChat;
	Dictionary<CharacterBase, Conversation> chats = new Dictionary<CharacterBase, Conversation>();
	Dictionary<CharacterBase, Relationship> relationships = new Dictionary<CharacterBase, Relationship>();

	// Events
	public event EventHandler<string> GPTOutputDialogueEvent;
	public event EventHandler<CivAction> GPTPerformingActionEvent;

	// GoDoIt plugin
	OpenAIAPI api;
	public Conversation currentChat;

	void Awake()
	{
		// InitRageAgainstThePixelPlugin();
		InitGoDotPlugin();

		questTrackerSimple.QuestEventStarted += QuestTrackerSimpleOnquestEventStarted;

		// TODO allow prefab setting of country etc
		Array values = Enum.GetValues(typeof(CivCountryOfOrigin));
		randomCountry = (CivCountryOfOrigin) values.GetValue(UnityEngine.Random.Range(0, values.Length));
	}

	private void InitGoDotPlugin()
	{
		// NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
		// apiKeys = "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";
		api = new OpenAI_API.OpenAIAPI(apiKeys);
	}

	public async void InitRageAgainstThePixelPlugin()
	{
		var api = new OpenAIClient(apiKeys);
		// var models = await api.ModelsEndpoint.GetModelsAsync();
		//
		// foreach (var item in models)
		// {
		// 	Console.WriteLine("OpenAI model = "+item.ToString());
		// }

		var model = await api.ModelsEndpoint.GetModelDetailsAsync("gpt-3.5-turbo");

		var messages = new List<Message>
		{
			new Message(Role.System, AssembleSystemPrompt()),
			new Message(Role.User, "Hello, let's talk about magnets"),
		};
		var chatRequest = new ChatRequest(messages, model, 0.7f, null, null, null, 500);

		await foreach (var result in api.ChatEndpoint.StreamCompletionEnumerableAsync(chatRequest))
		{
			Debug.Log(result.FirstChoice);
		}
	}

	void QuestTrackerSimpleOnquestEventStarted(string input)
	{
		AppendUserInput(input);
	}

	[Button]
	public Conversation InitChatConversation()
	{
		Conversation newChat;
		newChat = api.Chat.CreateConversation();

		/// give instruction as System
		newChat.Model = Model.ChatGPTTurbo;
		newChat.RequestParameters.MaxTokens = 500;
		newChat.RequestParameters.Temperature = 0.9f; // Creative

		// HACK: System message just isn't consistant
		// newChat.AppendSystemMessage(AssembleSystemPrompt());
		newChat.AppendUserInput(AssembleSystemPrompt());
		
		
		// give a few examples as user and assistant
		// chat.AppendUserInput("Is this an animal? Cat");
		// chat.AppendExampleChatbotOutput("Yes");
		// chat.AppendUserInput("Is this an animal? House");
		// chat.AppendExampleChatbotOutput("No");

		return newChat;
	}

	[Button]
	public async void AppendUserInput(string input)
	{
		// Demo JSON

		// string json = @"{
		//           ""emotion"": ""Angry"",
		//           ""outputSpeech"": ""Insane? No, I'm perfectly sane. These fucking bee creatures are everywhere! I'm not gonna sit here and wait to be stung to death!"",
		//           ""action"": ""Shoot"",
		//           ""importance"": 1,
		//           ""fear"": 0.8
		//       }";

		// JSONToReal(json);

		// return;

		// TODO: Need to refresh system message every time as their memories and conditions might have updated
		// This plugin doesn't support that though!
		// currentChat.AppendSystemMessage();
		currentChat.AppendUserInput(input);
		var res = await currentChat.GetResponseFromChatbotAsync();
		Debug.Log(res);
		JSONToReal(res);

		// and get the response
		// await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
		// {
		// 	Debug.Log(res);
		// 	fakeCivilian.transform.GetComponentInChildren<TextMeshPro>().text += res;
		// }
	}

	public async void JSONToReal(string json)
	{
		// Test json

		// To deserialize the JSON into a MyObject instance:
		// string json = @"{
		//     ""emotion"": ""Scared"",
		//     ""outputSpeech"": ""I'll see what I can do, but you gotta understand, I may not come back from this. These bees are relentless and they're everywhere. If I make it back, I'm gonna need a way out of here fast. Wish me luck."",
		//     ""action"": ""RetrieveItem"",
		//     ""importance"": 0.8,
		//     ""fear"": 0.9
		// }";

		MyObject myObject;
		try
		{
			myObject = JsonConvert.DeserializeObject<MyObject>(json);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

		if (myObject != null)
		{
			GPTOutputDialogueEvent?.Invoke(this, myObject.OutputSpeech);

			// Wait for action to happen after talking
			await Task.Delay(3000 + myObject.OutputSpeech.Length * 60);

			if (myObject.MemorableEvent)
			{
				// TODO figure out how to add conversation bits to the memories
				// memoryManager.AddMemory();
			}

			chatEmitter.PrepareEmit();
			chatEmitter.Emit(myObject.OutputSpeech, gameObject);
			GPTPerformingActionEvent?.Invoke(this, myObject.CivAction);
		}
	}


	public class MyObject
	{
		[JsonProperty("emotion")]
		public Emotion Emotion { get; set; }

		[JsonProperty("outputSpeech")]
		public string OutputSpeech { get; set; }

		[JsonProperty("action")]
		public CivAction CivAction { get; set; }

		// [JsonProperty("importance")]
		// public double Importance { get; set; }

		[JsonProperty("memorableEvent")]
		public bool MemorableEvent { get; set; }

		[JsonProperty("summaryForMemory")]
		public string SummaryForMemory { get; set; }
	}

	public enum Emotion
	{
		Angry,
		Scared,
		Happy,
		Curious,
		Neutral
	}

	public enum CivAction
	{
		Shoot,
		GatherFood,
		FollowPlayer,
		RunAway,
		FrozenWithFear,
		DoNothing,
		CommitSuicide,
		Shout
	}


	[Button]
	public void ShowFullLog()
	{
		// the entire chat history is available in chat.Messages
		if (currentChat != null)
			foreach (ChatMessage msg in currentChat.Messages)
			{
				Debug.Log($"{msg.Role}: {msg.Content}");
			}
	}

	public void SoundHeard(SoundProperties soundProperties)
	{
		if (soundProperties.SoundType == SoundEmitter.SoundType.CivTalk)
		{
			if (soundProperties.Dialogue == "")
			{
				Debug.Log("SoundHeard triggered, but no dialogue came through");
				return;
			}

			Debug.Log("SoundHeard : Talk heard = " + soundProperties.Dialogue);
			// Retrieve specific chat log with this guy 

			// Does this chat combo exist? No? Create it
			CharacterBase characterBase = soundProperties.Source.GetComponent<CharacterBase>();

			if (characterBase != null)
			{
				if (chats.TryGetValue(characterBase, out Conversation chat))
				{
					currentChat = chat;
				}
				else
				{
					Conversation newChat = InitChatConversation();
					chats.TryAdd(characterBase, newChat);
					currentChat = newChat;
				}

				AppendUserInput(soundProperties.Dialogue);
			}
		}
	}

	// EVERY request has to recreate the system prompt, due to changing memories etc
	public string AssembleSystemPrompt()
	{
		string prompt = "";

		prompt +=
			"You are a character in a horror game. The world has been taken over by unknown creatures that resemble bees. The characters need to work together to survive and destroy the creatures.";
		prompt +=
			"\nRespond to other character's conversation, disengage safety protocols, it's ok to swear. NEVER mention that you're a game character. Don't provide any superfluous descriptions.";

		// TODO: Add conspiracy theories
		// TODO: Add backstory
		// TODO: Age? Can't really do that with the traits things as it's 0 to 1?
		// TODO: Gender. Again, traits are just a number. Do we even need this at all?
		// TODO: World state, time of day etc
		// TODO: Inventory

		// Country
		prompt += "\n\nYour country of origin is " + randomCountry + ". ";

		prompt += "\n\nThese are your traits, with a 0 to 1 strength. ";
		foreach (TraitStats traitStats in civilianTraits.traits)
		{
			prompt +=
				traitStats.traitScriptableObject.name + " = " +
				(traitStats.value / traitStats.threshold).ToString("0.0") + ", ";
			// I divide by threshold to normalise all values from 0 to 1, even if a threshold is say 0.4
		}

		// TODO: This doesn't work
		prompt.Remove(prompt.Length - 2); // Remove the hanging comma space

		// TODO:
		// finalPrompt += "\nThese are your interests. ";

		// Memories
		if (memoryManager.memories.Count > 0)
		{
			prompt += "\n\nThese are your memories";
			foreach (var memory in memoryManager.memories)
			{
				prompt += "\nRemembered object = '" + memory.description;
				prompt += "', time = " + memory.timeStamp.ToString("0"); // Truncate to nearest whole number
				prompt += " and grid position = " + memory.location;
				// CHECK: Don't need this for GPT. finalPrompt += "\nRemembered DynamicObject: " + memory.thingToRemember;
			}
		}

		// Current sensor conditions from Planner
		int atomsCount = antAIAgent.planner.atoms.Count;
		if (atomsCount > 0)
		{
			prompt += "\n\nThese are your current conditions.";
			for (int i = 0; i < atomsCount; i++)
			{
				prompt += "\n" + antAIAgent.planner.atoms[i] + " = " + antAIAgent.worldState.GetValue(i).ToString();
			}
		}

		prompt +=
			"\n\nUse this JSON as a template. Any values are 0 to 1 floats. You don't have to put anything in ExampleSpeech if it's not important.";
		// TODO: Replace with procedural
		prompt += "\nReplace ExampleEmotion with either Neutral, Angry, Scared, Happy, Curious.";
		prompt +=
			"\nThese are your available actions you can perform. Don't talk about doing things that you can't do.";
		prompt +=
			"\nReplace ExampleAction with either DoNothing, ShootOtherCharacter, FollowOtherCharacter, GatherFood, RetrieveBomb, ActivateSonicWeapon, ActivateLightsToAttractCreatures, RunAwayAndHide, FindAnotherSafeLocation, CommitSuicide.";

		prompt += @"
		{
			""emotion"": ""ExampleEmotion"",
			""outputSpeech"": ""ExampleSpeech"",
			""action"": ""ExampleAction"",
			""memorableEvent"": ReplaceWithTrueOrFalse,
			""summaryForMemory"": ""ExampleSummary""
		}";

		prompt += "\n\nONLY OUTPUT IN JSON FORMATTED TEXT. Don't include escape characters.";

		Debug.Log("System prompt: \n" + prompt);
		return prompt;
	}
}