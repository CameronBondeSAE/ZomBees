using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CivGPT : MonoBehaviour
{
	public string apiKeys;

	OpenAIAPI api;

	[TextArea(5, 40)]
	public string systemMessage;

	[TextArea(5, 40)]
	public string prompt;

	OpenAI_API.Chat.Conversation chat;

	bool                      startedChat = false;
	public QuestTrackerSimple questTrackerSimple;

	public CivilianTraits civilianTraits;

	// View
	public TextMeshPro  textMeshProUGUI;
	public FakeCivilian fakeCivilian;
	public SimpleShoot  simpleShoot; // HACK

	void Awake()
	{
		Init();
	}

	private void Init()
	{
		// NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
		// apiKeys = "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";
		api = new OpenAI_API.OpenAIAPI(apiKeys);

		questTrackerSimple.questEventStarted += QuestTrackerSimpleOnquestEventStarted;
	}

	void QuestTrackerSimpleOnquestEventStarted(string obj)
	{
		AppendUserInput(obj);
	}

	[Button]
	public void StartChatConversation()
	{
		startedChat = true;
		if (api == null)
		{
			Init();
		}

		chat = api.Chat.CreateConversation();

		/// give instruction as System
		chat.Model                         = Model.ChatGPTTurbo;
		chat.RequestParameters.MaxTokens   = 500;
		chat.RequestParameters.Temperature = 0.9f; // Creative

		chat.AppendSystemMessage(systemMessage);

		// give a few examples as user and assistant
		// chat.AppendUserInput("Is this an animal? Cat");
		// chat.AppendExampleChatbotOutput("Yes");
		// chat.AppendUserInput("Is this an animal? House");
		// chat.AppendExampleChatbotOutput("No");
	}

	[Button]
	public async void AppendUserInput(string input)
	{
		if (startedChat == false)
			StartChatConversation();

		chat.AppendUserInput(input);


		var res = await chat.GetResponseFromChatbotAsync();

		Debug.Log(res);
		fakeCivilian.transform.GetComponentInChildren<TextMeshPro>().text = res; // HACK
		JSONToReal(res);

		// and get the response
		// await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
		// {
		// 	Debug.Log(res);
		// 	fakeCivilian.transform.GetComponentInChildren<TextMeshPro>().text += res;
		// }
	}

	public void JSONToReal(string json)
	{
		// To deserialize the JSON into a MyObject instance:
		// string json = @"{
		//     ""emotion"": ""Scared"",
		//     ""outputSpeech"": ""I'll see what I can do, but you gotta understand, I may not come back from this. These bees are relentless and they're everywhere. If I make it back, I'm gonna need a way out of here fast. Wish me luck."",
		//     ""action"": ""RetrieveItem"",
		//     ""importance"": 0.8,
		//     ""fear"": 0.9
		// }";

		MyObject myObject = JsonConvert.DeserializeObject<MyObject>(json);
		switch (myObject.Action)
		{
			case Action.Shoot:
				simpleShoot.Shoot();
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				GetComponent<Rigidbody>().AddForce(10f,5f,-15f);
				break;
			case Action.RetrieveItem:
				break;
			case Action.Hide:
				break;
			case Action.RunAway:
				break;
			case Action.FollowPlayer:
				break;
			case Action.Shout:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}


	public class MyObject
	{
		[JsonProperty("emotion")]
		public Emotion Emotion { get; set; }

		[JsonProperty("outputSpeech")]
		public string OutputSpeech { get; set; }

		[JsonProperty("action")]
		public Action Action { get; set; }

		[JsonProperty("importance")]
		public double Importance { get; set; }

		[JsonProperty("fear")]
		public double Fear { get; set; }
	}

	public enum Emotion
	{
		Scared,
		Happy,
		Anxious
	}

	public enum Action
	{
		Shoot,
		RetrieveItem,
		Hide,
		RunAway,
		FollowPlayer,
		Shout
	}


	[Button]
	public void ShowFullLog()
	{
		// the entire chat history is available in chat.Messages
		foreach (ChatMessage msg in chat.Messages)
		{
			Debug.Log($"{msg.Role}: {msg.Content}");
		}
	}
}