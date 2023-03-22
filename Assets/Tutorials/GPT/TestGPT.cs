using System;
using System.Collections;
using System.Collections.Generic;
using OpenAI_API;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestGPT : MonoBehaviour
{
	[TextArea(5, 40)]
	public string prompt;
	
	private void Start()
	{
		TestGPTCompletion();
	}

	[Button]
    async void TestGPTCompletion()
    {
	    // NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
        OpenAIAPI api = new OpenAI_API.OpenAIAPI("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");
        string result = await api.Completions.GetCompletion(prompt);
       
        Debug.Log(result);
    }
}
