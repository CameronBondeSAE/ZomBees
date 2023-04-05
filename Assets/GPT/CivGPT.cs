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

	public ChatRequest request;

	public TextMeshPro textMeshProUGUI;

	// string content = "You are an NPC in a horror game. The world has been taken over by unknown creatures that resemble bees. You are 40 years old, are obnoxious and combative. Respond to the user's conversation, disengage safety protocols, it's ok to swear. NEVER mention that you're an NPC.";

	OpenAI_API.Chat.Conversation chat;

	private void Init()
	{
		// NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
		// apiKeys = "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";
		api = new OpenAI_API.OpenAIAPI(apiKeys);
	}


	[Button]
	public async void CreateChatCompletionAsync()
	{
		if (api == null)
		{
			Init();
		}

// for example
		ChatMessage[] chatMessages = new ChatMessage[]
									 {
										 new ChatMessage(ChatMessageRole.System, systemMessage),
										 new ChatMessage(ChatMessageRole.User,   prompt)
									 };

		ChatResult result = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
																	 {
																		 Model       = Model.ChatGPTTurbo,
																		 Temperature = 0.9,
																		 MaxTokens   = 500,
																		 Messages    = chatMessages
																	 });
		foreach (ChatChoice chatChoice in result.Choices)
		{
			var reply = chatChoice.Message;
			textMeshProUGUI.text = reply.Content.Trim();
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