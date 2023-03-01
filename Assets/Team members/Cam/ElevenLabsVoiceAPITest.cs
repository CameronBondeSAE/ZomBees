using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ElevenLabsVoiceAPITest : MonoBehaviour
{
	string         voiceID = "21m00Tcm4TlvDq8ikWAM";
	string         apiUrl  = "https://api.elevenlabs.io/v1/text-to-speech/";
	private string apiKey  = "11ce6c73cc5e0a806f38b60e31d7cae5";

	public AudioSource audioSource;

	
	public string text             = "bums are great";
	public int    stability        = 0;
	public int    similarity_boost = 0;


	[Button]
	public void SynthesizeSpeech()
	{
		StartCoroutine(SendSynthesizeRequest());
	}
	
	IEnumerator SendSynthesizeRequest()
	{
		// Set up the request
		string          url     = apiUrl+voiceID;
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		request.SetRequestHeader("accept",       "audio/mpeg");
		request.SetRequestHeader("xi-api-key",   "11ce6c73cc5e0a806f38b60e31d7cae5");
		request.SetRequestHeader("Content-Type", "application/json");

		// Set up the request data
		string jsonRequestBody = "{\"text\":\"" + text + "\",\"voice_settings\":{\"stability\":" + stability + ",\"similarity_boost\":" + similarity_boost + "}}";
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
}