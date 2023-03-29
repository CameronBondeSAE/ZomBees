using System;
using System.Collections;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using UnityEngine;

public class TestGPT : MonoBehaviour
{
	OpenAIAPI api;

	[TextArea(5, 40)]
	public string prompt;

	public ChatRequest request;

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
		
		string    result = await api.Completions.GetCompletion(prompt);
       
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
}
