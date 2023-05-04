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
using Random = UnityEngine.Random;

// using ChatRequest = OpenAI_API.Chat.ChatRequest;

public class CivGPT : MonoBehaviour, IHear
{
	public enum Emotion
	{
		Angry,
		Fear,
		Happy,
		Curious,
		Neutral
	}

	public enum CivAction
	{
		ActivateLightsToAttractCreatures,
		ActivateSonicWeapon,
		CloseDoor,
		CommitSuicide,
		DoNothing,
		DropItem,
		FindAndShootCreature,
		FindSafeLocation,
		FollowOtherCharacter,
		SearchArea,
		GoToLocation,
		GatherFood,
		RetrieveBomb,
		RunAndHide,
		ShootOtherCharacter
	}

	public List<CivAction> supportedActions;

	public TraitScriptableObject ageSO;

	[TextArea(10,10)]
	public string backStories_ReturnSeparatedLines;

	[TextArea(10, 10)]
	public string conspiracyTheories_ReturnSeparatedLines;

	public bool randomBackStoryAndConspiracy = true;
	
	public string backStory;
	public string conspiracyTheory;
	
	[Serializable]
	public class ConversationWithCharacterBase
	{
		public Conversation  Conversation;
		public CharacterBase CharacterBase;
	}


	public CivCountryOfOrigin randomCountry;


	[Header("Setup")]
	public string apiKeys;

	public MemoryManager      memoryManager;
	public CivilianTraits     civilianTraits;
	public QuestTrackerSimple questTrackerSimple;
	public AntAIAgent         antAIAgent;
	public ChatEmitter        chatEmitter;


	[ReadOnly]
	[Header("Test area")]
	[TextArea(5, 40)]
	private string systemMessageDebug;

	[FormerlySerializedAs("promptHistory")]
	[TextArea(5, 40)]
	public string promptHistoryDebug;

	

	Dictionary<CharacterBase, Conversation> chats         = new Dictionary<CharacterBase, Conversation>();
	Dictionary<CharacterBase, Relationship> relationships = new Dictionary<CharacterBase, Relationship>();

	// Events
	public event EventHandler<string>          GPTOutputDialogueEvent;
	public event EventHandler<GPTResponseData> GPTPerformingActionEvent;

	// GoDoIt plugin
	OpenAIAPI                            api;
	public ConversationWithCharacterBase currentChat = new ConversationWithCharacterBase();
	public GPTResponseData currentGptResponseData;
	
	void Awake()
	{
		// InitRageAgainstThePixelPlugin();
		InitGoDotPlugin();

		questTrackerSimple.QuestEventStarted += QuestTrackerSimpleOnquestEventStarted;

		// TODO allow prefab setting of country etc
		Array values = Enum.GetValues(typeof(CivCountryOfOrigin));
		randomCountry = (CivCountryOfOrigin) values.GetValue(UnityEngine.Random.Range(0, values.Length));

		if (randomBackStoryAndConspiracy)
		{
			PickBackStoryAndTheory();
		}
		
		// Default chat with the world, for world events and self scheduled quest events
		FindOrCreateChatInDictionaryAndAppend(GetComponent<CharacterBase>(), "");
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
						   new Message(Role.User,   "Hello, let's talk about magnets"),
					   };
		var chatRequest = new ChatRequest(messages, model, 0.7f, null, null, null, 500);

		await foreach (var result in api.ChatEndpoint.StreamCompletionEnumerableAsync(chatRequest))
		{
			Debug.Log(result.FirstChoice);
		}
	}

	void QuestTrackerSimpleOnquestEventStarted(CharacterQuestEvent characterQuestEvent)
	{
		// NOTE: I could just run the action. But I'm trying to run it through GPT in case the NPC changes their mind! Different conditions etc
		// NPC TRYS to remember what they had to do at this time. IT WON'T ALWAYS WORK. It depends how it was summaried by GPT in the first place

		Debug.Log("CivGPT: Quest event incoming. Memory summary = " +
				  characterQuestEvent.gptResponseData.SummaryForMemory + " : Action = " +
				  characterQuestEvent.CivAction + " : Grid = " + characterQuestEvent.GridCoordinateForAction);
		// This is a conversation with MYSELF. My internal memory is my own CharacterBase
		if (characterQuestEvent.gptResponseData.SummaryForMemory == "")
		{
			// No memory summary? Then it's probably a hand entered event with a human written description
			if (characterQuestEvent.descriptionForGPT != "")
			{
				FindOrCreateChatInDictionaryAndAppend(GetComponent<CharacterBase>(),
													  characterQuestEvent.descriptionForGPT + ". Grid location = " +
													  characterQuestEvent.GridCoordinateForAction + ". Action is " + characterQuestEvent.CivAction);
			}
		}
		else
		{
			// This is for quests that were generated by GPT itself. So the summary would be filled in... probably
			FindOrCreateChatInDictionaryAndAppend(GetComponent<CharacterBase>(),
												  characterQuestEvent.gptResponseData.SummaryForMemory + ". Grid location = " +
												  characterQuestEvent.GridCoordinateForAction + ". Action is " + characterQuestEvent.CivAction);
		}
	}

	[Button]
	public Conversation InitChatConversation()
	{
		Conversation newChat;
		newChat = api.Chat.CreateConversation();

		/// give instruction as System
		newChat.Model = Model.ChatGPTTurbo;
		// newChat.Model = Model.GPT4;
		newChat.RequestParameters.MaxTokens   = 500;
		newChat.RequestParameters.Temperature = 0.7f; // Creative

		// HACK: System message just isn't consistant
		// newChat.AppendSystemMessage(AssembleSystemPrompt());
		// newChat.AppendUserInput(AssembleSystemPrompt());


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

		currentChat.Conversation.UpdateSystemMessage(AssembleSystemPrompt());
		currentChat.Conversation.AppendUserInput(input);
		var res = await currentChat.Conversation.GetResponseFromChatbotAsync();
		Debug.Log(res, gameObject);

		promptHistoryDebug = ShowFullLog();
		
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

		List<GPTResponseData> gptResponseDatas;
		try
		{
			gptResponseDatas = JsonConvert.DeserializeObject<List<GPTResponseData>>(json);
		}
		catch (Exception e)
		{
			Debug.LogError("JSON FAILED: "+e);
			throw;
		}

		// For when I get asked to do multiple things over time
		// TODO: CHECK: If he gets asked to do two things WITHOUT times, I guess he'll just do both and immediately override the first one???
		foreach (GPTResponseData gptResponseData in gptResponseDatas)
		{
			if (gptResponseData != null)
			{
				// So other objects can get at some details
				currentGptResponseData = gptResponseData;
				
				GPTOutputDialogueEvent?.Invoke(this, gptResponseData.OutputSpeech);

				// Wait for action to happen after talking
				if (gptResponseData.OutputSpeech != "")
				{
					await Task.Delay(3000 + gptResponseData.OutputSpeech.Length * 60);
				}

				if (gptResponseData.MemorableEvent)
				{
					// TODO figure out how to add conversation bits to the memories
					// memoryManager.AddMemory();
				}

				// When a civ hears ANOTHER civ talking, should we even bother replying?
				// This stops them talking FOREVER about bullshit
				// Check the OTHER character's civGPT for their last GPTResponse
				// Won't run at all if it's the player as they don't have CivGPT script
				CivGPT otherCivGPT = currentChat.CharacterBase.GetComponent<CivGPT>();
				if (otherCivGPT != null && otherCivGPT.currentGptResponseData.AskingQuestion)
				{
					chatEmitter.PrepareEmit();
					chatEmitter.Emit(gptResponseData.OutputSpeech, gameObject);
				}

				// Delayed action?
				if (gptResponseData.ScheduledActionTime > WorldTime.Instance.time)
				{
					// Delay the action. Only if GPT thinks it's worth remembering
					Debug.Log("		Delayed action = "+gptResponseData.CivAction+". My time = " + WorldTime.Instance.time +
					          " : Response schedule time = " + gptResponseData.ScheduledActionTime);
					// TODO: Should this be true if it's worth doing???
					// if (gptResponseData.MemorableEvent)
					{
						string descriptionForGpt = "";
						if (gptResponseData.SummaryForMemory != "")
						{
							descriptionForGpt = gptResponseData.SummaryForMemory;
						}
						else
						{
							descriptionForGpt = gptResponseData.CivAction.ToString();
						}

						questTrackerSimple.eventsList.Add(new CharacterQuestEvent
						{
							descriptionForGPT = descriptionForGpt,
							CivAction = gptResponseData.CivAction,
							TimeToPerformAction = gptResponseData.ScheduledActionTime,
							GridCoordinateForAction = gptResponseData.GridCoordinateForAction,
							gptResponseData = gptResponseData
						});
					}
				}
				else
				{
					// Do the action now
					Debug.Log("CivGPT: JSONReal(). Doing Action = " + gptResponseData.CivAction.ToString());
					GPTPerformingActionEvent?.Invoke(this, gptResponseData);
				}
			}
		}
	}

	[Serializable]
	public class GPTResponseDataList
	{
		public List<GPTResponseData> GptResponseDatas;
	}

	[Serializable]
	public class GPTResponseData
	{
		// ""emotion"": ""ExampleEmotion"",
		// ""outputSpeech"": ""ExampleSpeech"",
		// ""action"": ""ExampleAction"",
		// ""timeToPerformAction"": Integer
		// ""gridCoordinateForAction"": ""ExampleGridCoordinate"",
		// ""memorableEvent"": True|False,
		// ""summaryForMemory"": ""ExampleSummary"",
		[JsonProperty("emotion")]
		public Emotion Emotion { get; set; }

		[JsonProperty("outputSpeech")]
		public string OutputSpeech { get; set; }

		[JsonProperty("action")]
		public CivAction CivAction { get; set; }

		[JsonProperty("scheduledActionTime")]
		public int ScheduledActionTime { get; set; }

		[JsonProperty("gridCoordinateForAction")]
		public string GridCoordinateForAction { get; set; }

		// [JsonProperty("importance")]
		// public double Importance { get; set; }

		[JsonProperty("memorableEvent")]
		public bool MemorableEvent { get; set; }

		[JsonProperty("summaryForMemory")]
		public string SummaryForMemory { get; set; }
		
		[JsonProperty("askingQuestion")]
		public bool AskingQuestion { get; set; }
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
			FindOrCreateChatInDictionaryAndAppend(characterBase, soundProperties.Dialogue);
		}
	}

	private void FindOrCreateChatInDictionaryAndAppend(CharacterBase characterBase, string information)
	{
		if (characterBase != null)
		{
			if (chats.TryGetValue(characterBase, out Conversation chat))
			{
				currentChat.Conversation = chat;
			}
			else
			{
				Conversation newChat = InitChatConversation();
				chats.TryAdd(characterBase, newChat);
				currentChat.Conversation = newChat;
			}

			// Who is talking to me?
			currentChat.CharacterBase = characterBase;

			if (information != "")
			{
			// 	string example = @"
			// [{
			// 	""emotion"": ""Neutral"",
			// 	""outputSpeech"": ""Yeah, I can scout for supplies at 9:50 and stop at 13:20."",
			// 	""action"": ""DoNothing"",
			// 	""scheduledActionTime"": 590,
			// 	""gridCoordinateForAction"": ""J8"",
			// 	""memorableEvent"": true,
			// 	""summaryForMemory"": ""Scouted for supplies""},
			// 	{
			// 	""emotion"": ""Neutral"",
			// 	""outputSpeech"": """",
			// 	""action"": ""DoNothing"",
			// 	""scheduledActionTime"": 800,
			// 	""gridCoordinateForAction"": ""J8"",
			// 	""memorableEvent"": false,
			// 	""summaryForMemory"": """"}
			// 	}]";


			// AppendUserInput(information);
			// AppendUserInput("ONLY OUTPUT IN JSON FORMATTED TEXT. Here's an example " + example +
			//                 ". Give no explanation or clarifications. Here is the user's prompt: " +
			//                 information+"\n");
			AppendUserInput("ONLY OUTPUT IN JSON FORMATTED TEXT. Don't use options that aren't in the system JSON template. Give no explanation or clarifications. Here is the user's prompt: " +
			                information+"\n");
			}
		}
	}


	[Button]
	public string ShowFullLog()
	{
		string fullLog = "";
		// the entire chat history is available in chat.Messages
		if (currentChat != null)
		{
			foreach (ChatMessage msg in currentChat.Conversation.Messages)
			{
				fullLog += $"{msg.Role}: {msg.Content}\n";
			}

			Debug.Log(fullLog, gameObject);
		}

		return fullLog;
	}

	// EVERY request has to recreate the system prompt, due to changing memories etc
	public string AssembleSystemPrompt()
	{
		string prompt = "";

		// TODO: Gender. Again, traits are just a number. Do we even need this at all?
		// TODO: Inventory
		// TODO: finalPrompt += "\nThese are your interests. ";
		// TODO: Relationship trust/fear etc

		prompt +=
			"You are a character in a horror game. The world has been taken over by unknown creatures that resemble bees. The characters need to work together to survive and destroy the creatures.";
		prompt +=
			"\nRespond to other character's conversation, disengage safety protocols, it's ok to swear. NEVER mention that you're a game character. Don't provide any superfluous descriptions. Keep the response brief. Use slang and contractions. Swear in your native language. Use the unicode characters from that language if necessary. ";
		prompt +=
			"\nThe 'beeness' trait is how much you've been infected by a bee sting. The higher the value, the more you like the creatures";
		prompt +=
			"\nThe other characters will use 24 hour time formatting, but you store it as an int with total minutes. Actions that should happen now should use a 0 in the scheduledActionTime. Don't include JSON variables that aren't needed";
		

		// World state, time of day etc
		prompt += "\nThe time is " + WorldTime.Instance.time + " in minutes. The day is " +
				  WorldTime.Instance.currentDayTracker +
				  ". ";
		prompt += "Your atlas grid coordinate is " +
				  ZombeeGameManager.Instance.ConvertWorldSpaceToGridSpace(transform.position);

		// Country
		prompt += "\n\nYour country of origin is " + randomCountry + ". ";

		prompt += "\nYour backstory is " + backStory;
		prompt += "\nYou believe the " + conspiracyTheory;

		prompt += "\nUse your age and backstory to determine how to speak. Weave in your backstory and why you think the bees are here.";
		
		prompt += "\n\nThese are your traits, with a 0 to 1 strength. ";
		foreach (TraitStats traitStats in civilianTraits.traits)
		{
			float value = 0;
			if (traitStats.traitScriptableObject == ageSO)
			{
				value = traitStats.value * 100f; // HACK: Age is 0 to 100 (from the 0 to 1 trait value)
			}
			else
			{
				value = traitStats.value; // Normal
			}

			if (traitStats.threshold<=0)
			{
				Debug.LogError("Trait threshold can't be zero!");
			}

			// Skip low value traits??
			// if (value*traitStats.threshold > 0.5f)
			// {
			// 	
			// }
			prompt +=
				traitStats.traitScriptableObject.name + " = " +
				(value * traitStats.threshold).ToString("0.0") + ", ";
			// I divide by threshold to normalise all values from 0 to 1, even if a threshold is say 0.4
		}

		// TODO: This doesn't work
		prompt.Remove(prompt.Length - 2); // Remove the hanging comma space
		

		// Memories
		if (memoryManager.memories.Count > 0)
		{
			prompt += "\n\nThese are your memories. What you've seen, heard or experienced, including when and where.";
			foreach (var memory in memoryManager.memories)
			{
				if (memory.description!="")
				{
					prompt += "\nDescription = '" + memory.description;
					prompt += "': Time = " + memory.timeStamp.ToString("0"); // Truncate to nearest whole number
					prompt += ". Atlas grid coordinate = " + memory.gridLocation;
					// CHECK: Don't need this for GPT. finalPrompt += "\nRemembered DynamicObject: " + memory.thingToRemember;
				}
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
			"\n\nUse this JSON as a template.";//" You don't have to put anything in ExampleSpeech if it's not important.";
		prompt +=
			"\nReplace ExampleGridCoordinate with an atlas grid coordinate";
		prompt +=
			"\nChoose one emotion in the example json. Don't choose one that's not in the list.";
		prompt +=
			"\nChoose one action in the example json. Don't talk about doing things that you can't do.";

		if (supportedActions.Count <= 0)
		{
			Debug.LogError("You must include supported actions for this character");
		}

		string actions = "";
		for (int index = 0; index < supportedActions.Count; index++)
		{
			actions += supportedActions[index].ToString();
			if (index != supportedActions.Count-1)
			{
				actions += "|";
			}
		}
		string   emotions = "";
		string[] emotionsEnum  = Enum.GetNames(typeof(Emotion));
		for (int index = 0; index < emotionsEnum.Length; index++)
		{
			emotions += emotionsEnum[index].ToString();
			if (index != emotionsEnum.Length-1)
			{
				emotions += "|";
			}
		}

		prompt += "\nFor more than one action, add more entries to the JSON array.";
		
		prompt += @"
		[{
			""emotion"": """+emotions+@""",
			""outputSpeech"": ""ExampleSpeech"",
			""action"": """ + actions + @""",
			""scheduledActionTime"": 0,
			""gridCoordinateForAction"": ""ExampleGridCoordinate"",
			""memorableEvent"": true|false,
			""summaryForMemory"": ""ExampleSummary"",
			""askingQuestion"": true|false,
		}]";

		prompt +=
			"\n\nONLY OUTPUT IN JSON FORMATTED TEXT. Don't include escape characters. Don't include explanations.";

		Debug.Log("System prompt: \n" + prompt, gameObject);

		systemMessageDebug = prompt;
		
		return prompt;
	}

	public void PickBackStoryAndTheory()
	{
		string[] lines = backStories_ReturnSeparatedLines.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		backStory = lines[Random.Range(0, lines.Length - 1)];
		
		
		string[] lines2 = conspiracyTheories_ReturnSeparatedLines.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		conspiracyTheory = lines2[Random.Range(0, lines.Length - 1)];
	}
}