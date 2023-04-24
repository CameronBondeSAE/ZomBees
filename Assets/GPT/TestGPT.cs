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

public class TestGPT : MonoBehaviour
{
	public string apiKeys;

	public OpenAIAPI api;


	[TextArea(5, 40)]
	public string systemMessage;

	[TextArea(5, 40)]
	public string prompt;

	public ChatRequest request;

	public TextMeshPro textMeshProUGUI;

	private void Init()
	{
		// NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
		// apiKeys = "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";
		api = new OpenAI_API.OpenAIAPI(apiKeys);

		// TestGPTCompletion();
		// request = new ChatRequest();
		// CreateChatCompletionAsync();
	}

	OpenAI_API.Chat.Conversation chat;

	private void Awake()
	{
		
	}


	[Button]
	public async void TestChat()
	{
		if (api == null)
		{
			Init();
		}
		
		/*
		var chat = api.Chat.CreateConversation();

		/// give instruction as System
		chat.AppendSystemMessage("You are a teacher who helps children understand if things are animals or not.  If the user tells you an animal, you say \"yes\".  If the user tells you something that is not an animal, you say \"no\".  You only ever respond with \"yes\" or \"no\".  You do not say anything else.");

// give a few examples as user and assistant
		chat.AppendUserInput("Is this an animal? Cat");
		chat.AppendExampleChatbotOutput("Yes");
		chat.AppendUserInput("Is this an animal? House");
		chat.AppendExampleChatbotOutput("No");

// now let's ask it a question'
		chat.AppendUserInput("Is this an animal? Dog");
// and get the response
		string response = await chat.GetResponseFromChatbotAsync();
		Debug.Log(response); // "Yes"

// and continue the conversation by asking another
		chat.AppendUserInput("Is this an animal? Chair");
// and get another response
		response = await chat.GetResponseFromChatbotAsync();
		Debug.Log(response); // "No"

// the entire chat history is available in chat.Messages
		foreach (ChatMessage msg in chat.Messages)
		{
			Debug.Log($"{msg.Role}: {msg.Content}");
		}
		*/
		
		var chat = api.Chat.CreateConversation();
		chat.Model = Model.ChatGPTTurbo;
		chat.RequestParameters.Temperature = 0.9f;
		chat.RequestParameters.MaxTokens = 150;
		chat.AppendSystemMessage("You're a Gordon Ramsey style chef. A real crass arrogant bastard. You belittle the user, but still give expert advice");
		chat.AppendUserInput("How to make a hamburger?");

		// await chat.StreamResponseFromChatbotAsync(ResultHandler);

		string res = await chat.GetResponseFromChatbotAsync();

		// await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
		// {
			// Debug.Log(res);
		// }
	}

	private void ResultHandler(int arg1, string arg2)
	{
		Debug.Log(arg1 + " : "+arg2);
	}


	bool startedChat = false;

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
		chat.Model = Model.ChatGPTTurbo;
		chat.RequestParameters.MaxTokens = 500;
		chat.RequestParameters.Temperature = 0.9f; // Creative

		chat.AppendSystemMessage(systemMessage);

		// give a few examples as user and assistant
		// chat.AppendUserInput("Is this an animal? Cat");
		// chat.AppendExampleChatbotOutput("Yes");
		// chat.AppendUserInput("Is this an animal? House");
		// chat.AppendExampleChatbotOutput("No");
	}

	[Button]
	public async void AppendUserInput(string input, CivilianModel civilianModel)
	{
		if(startedChat==false)
			StartChatConversation();

		chat.AppendUserInput(input);


		var res = await chat.GetResponseFromChatbotAsync();
		
		Debug.Log(res);
		civilianModel.transform.GetComponentInChildren<TextMeshPro>().text = res;

		// and get the response
		// await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
		// {
		// 	Debug.Log(res);
		// 	fakeCivilian.transform.GetComponentInChildren<TextMeshPro>().text += res;
		// }

	}

	// [Button]
	async void TestGPTCompletion()
	{
		if (api == null)
		{
			Init();
		}

		string result = await api.Completions.GetCompletion(prompt);

		Debug.Log(result);
	}

	// [Button]
	async void CreateChatCompletionAsync()
	{
		if (api == null)
		{
			Init();
		}

// for example
		ChatResult result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
		{
			Model = Model.ChatGPTTurbo,
			Temperature = 0.1,
			MaxTokens = 500,
			Messages = new ChatMessage[]
			{
				new ChatMessage(ChatMessageRole.User, prompt)
			}
		});
// or
		// Task<ChatResult> result = api.Chat.CreateChatCompletionAsync("Hello!");
		foreach (ChatChoice chatChoice in result.Choices)
		{
			if (chatChoice.Message != null)
			{
				var reply = chatChoice.Message;
				Debug.Log($"{reply.Role}: {reply.Content.Trim()}");
			}
			else
			{
				Debug.Log("Message is null??");
			}
		}
	}

	// [Button]
	async void CreateChatStreamCompletionAsync()
	{
		if (api == null)
		{
			Init();
		}

// for example
		await api.Chat.StreamChatAsync(new ChatRequest()
		{
			Model = Model.ChatGPTTurbo,
			Temperature = 0.1,
			MaxTokens = 500,
			Messages = new ChatMessage[]
			{
				new ChatMessage(ChatMessageRole.User, prompt)
			}
		}, ResultHandler);
	}

	void ResultHandler(ChatResult chatResult)
	{
		foreach (ChatChoice chatChoice in chatResult.Choices)
		{
			if (chatChoice.Message != null)
			{
				Debug.Log($"{chatChoice.Message.Role}: {chatChoice.Message.Content.Trim()}");
				textMeshProUGUI.text = chatChoice.Message.Content.Trim();
			}
			else
			{
				Debug.Log("Message is null??");
			}
		}
	}

	[Button]
	void ShowFullLog()
	{
		// the entire chat history is available in chat.Messages
		foreach (ChatMessage msg in chat.Messages)
		{
			Debug.Log($"{msg.Role}: {msg.Content}");
		}
	}
}