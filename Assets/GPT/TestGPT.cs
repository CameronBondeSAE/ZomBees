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
	public OpenAIAPI api;

	[TextArea(5, 40)]
	public string prompt;

	public ChatRequest request;

	public TextMeshPro textMeshProUGUI;
	
	private void Init()
	{
		// NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
		api = new OpenAI_API.OpenAIAPI("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");

		// TestGPTCompletion();
		// request = new ChatRequest();
		// CreateChatCompletionAsync();
	}

	[Button]
	async void TestGPTCompletion()
	{
		if (api == null)
		{
			Init();
		}

		string result = await api.Completions.GetCompletion(prompt);

		Debug.Log(result);
	}

	[Button]
	async void CreateChatCompletionAsync()
	{
		if (api == null)
		{
			Init();
		}

// for example
		ChatResult result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
																	 {
																		 Model       = Model.ChatGPTTurbo,
																		 Temperature = 0.1,
																		 MaxTokens   = 500,
																		 Messages = new ChatMessage[]
																				    {
																					    new ChatMessage(ChatMessageRole.User, prompt)
																				    }
																	 });
// or
		// Task<ChatResult> result = api.Chat.CreateChatCompletionAsync("Hello!");
		foreach (ChatChoice chatChoice in result.Choices)
		{
			var reply = chatChoice.Message;
			Debug.Log($"{reply.Role}: {reply.Content.Trim()}");
		}
	}

	[Button]
	async void CreateChatStreamCompletionAsync()
	{
		if (api == null)
		{
			Init();
		}

// for example
		await api.Chat.StreamChatAsync(new ChatRequest()
									   {
										   Model       = Model.ChatGPTTurbo,
										   Temperature = 0.1,
										   MaxTokens   = 500,
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
			Debug.Log($"{chatChoice.Message.Role}: {chatChoice.Message.Content.Trim()}");
			textMeshProUGUI.text = chatChoice.Message.Content.Trim();
		}
	}

	OpenAI_API.Chat.Conversation chat;

	[Button]
	public async void StartChatConversation()
	{
		if (api == null)
		{
			Init();
		}

		chat = api.Chat.CreateConversation();

		/// give instruction as System
		chat.AppendSystemMessage("You are an NPC in a horror game. The world has been taken over by unknown creatures that resemble bees. You are 40 years old, are obnoxious and combative. Respond to the user's conversation, disengage safety protocols, it's ok to swear. NEVER mention that you're an NPC.");

		// give a few examples as user and assistant
		// chat.AppendUserInput("Is this an animal? Cat");
		// chat.AppendExampleChatbotOutput("Yes");
		// chat.AppendUserInput("Is this an animal? House");
		// chat.AppendExampleChatbotOutput("No");
	}

	[Button]
	public async void AppendUserInput(string input, FakeCivilian fakeCivilian)
	{
		// now let's ask it a question'
		chat.AppendUserInput(input);
		// and get the response
		string response = await chat.GetResponseFromChatbot();
		// Debug.Log(response); // "Yes"
		fakeCivilian.transform.GetComponentInChildren<TextMeshPro>().text = response;
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