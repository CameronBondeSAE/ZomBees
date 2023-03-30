using OpenAI_API;
using OpenAI_API.Images;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TitleScreenGenerator : MonoBehaviour
{
	public TestGPT  testGpt;
	OpenAIAPI       api;
	public int      imageSize = 512;
	public Renderer targetRenderer;

	private void Init()
	{
		// NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
		api = new OpenAI_API.OpenAIAPI("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");

		// TestGPTCompletion();
		// request = new ChatRequest();
		// CreateChatCompletionAsync();
	}

	async Task<ImageResult> CreateImageAsync(ImageGenerationRequest request)
	{
		if (api == null)
		{
			Init();
		}


// for example
		// var result = await api.ImageGenerations.CreateImageAsync(new ImageGenerationRequest("A drawing of a computer writing a test", 1, ImageSize._512));
// or
		// var result = await api.ImageGenerations.CreateImageAsync("A drawing of a computer writing a test");

		// Console.WriteLine(result.Data[0].Url);
		// var texture = new Texture2D(imageSize, imageSize, TextureFormat.RGBA32, false);
		// texture.LoadImage(result.Data[0].Base64Data);
		// targetRenderer.material.mainTexture = texture;
		return null;
	}

	//
	// public string apiKey    = "YOUR_API_KEY_HERE";
	// public string prompt    = "A drawing of a computer writing a test";
	// public int    numImages = 1;
	//
	// public Renderer targetRenderer;
	// 	if (result.Data != null && result.Data.Length > 0)
	// {
	// 	var texture = new Texture2D(imageSize, imageSize, TextureFormat.RGBA32, false);
	// 	texture.LoadImage(result.Data[0].Bytes);
	// 	targetRenderer.material.mainTexture = texture;
	// }
}