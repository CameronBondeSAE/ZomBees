using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ElevenLabsVoiceAPITest : MonoBehaviour
{
	string         apiUrl = "https://api.elevenlabs.io/v1/text-to-speech/21m00Tcm4TlvDq8ikWAM";
	private string apiKey = "11ce6c73cc5e0a806f38b60e31d7cae5";

	public AudioSource audioSource;

	[Button]
	public void SynthesizeSpeech(string textToSynthesize)
	{
		StartCoroutine(SendSynthesizeRequest2(textToSynthesize));
	}
	
	IEnumerator SendSynthesizeRequest2(string textToSynthesize)
	{
		// Set up the request
		string          url     = "https://api.elevenlabs.io/v1/text-to-speech/21m00Tcm4TlvDq8ikWAM";
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		request.SetRequestHeader("accept",       "audio/mpeg");
		request.SetRequestHeader("xi-api-key",   "11ce6c73cc5e0a806f38b60e31d7cae5");
		request.SetRequestHeader("Content-Type", "application/json");

		// Set up the request data
		string jsonRequestBody = "{\"text\": \"Waaaz happnin boyeeee!!!\",\"voice_settings\": {\"stability\": 0,\"similarity_boost\": 0}}";
		byte[] bodyRaw         = new System.Text.UTF8Encoding(true).GetBytes(jsonRequestBody);
		request.uploadHandler   = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerAudioClip("", AudioType.MPEG);

		// Send the request
		yield return request.SendWebRequest();

		// Handle the response
		if (request.result == UnityWebRequest.Result.Success)
		{
			AudioClip audioClip = DownloadHandlerAudioClip.GetContent(request);
			// Do something with the audio clip
			audioSource.clip = audioClip;
			audioSource.Play();
		}
		else
		{
			Debug.LogError("Error sending text-to-speech request: " + request.error);
		}
	}
	
	
	private IEnumerator SendSynthesizeRequest(string textToSynthesize)
	{
		string jsonRequest = "{\"text\":\"" + textToSynthesize + "\"}";

		using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, jsonRequest))
		{
			www.SetRequestHeader("accept",       "audio/mpeg");
			www.SetRequestHeader("xi-api-key",   apiKey);
			www.SetRequestHeader("Content-Type", "application/json");

			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError(www.error);
			}
			else
			{
				byte[] audioBytes = www.downloadHandler.data;
				audioSource.clip = WavUtility.ToAudioClip(audioBytes);
				audioSource.Play();
			}
		}
	}
}