using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ElevenLabsVoiceAPITest : MonoBehaviour
{
	public VoiceList voiceList;

	// public string[] voiceID = new string[] {"21m00Tcm4TlvDq8ikWAM",};
	string apiUrl = "https://api.elevenlabs.io/v1/text-to-speech/";
	private string apiKey = "6fee898400ed59fa0e53d71ed9e585e0";

	public AudioSource audioSource;

	public int stability = 0;
	public int similarity_boost = 0;
	[SerializeField]
	private bool testMode = false;

	[SerializeField]
	public bool multiLingualVoice = true;


	[Button]
	public void PlayTheLastVoiceReceived()
	{
		audioSource.Play();
	}
	
	private void Awake()
	{
		GetVoices();
	}

	[Button]
	void GetVoices()
	{
		StartCoroutine(GetVoicesCoroutine());
	}

	IEnumerator GetVoicesCoroutine()
	{
		UnityWebRequest www = UnityWebRequest.Get("https://api.elevenlabs.io/v1/voices");
		www.SetRequestHeader("accept", "application/json");
		www.SetRequestHeader("xi-api-key", apiKey);

		yield return www.SendWebRequest();

		if (www.result != UnityWebRequest.Result.Success)
		{
			Debug.Log("Error: " + www.error);
		}
		else
		{
			Debug.Log("Response: " + www.downloadHandler.text);
			ReadJSON(www.downloadHandler.text);
		}
	}

	[System.Serializable]
	public class Voice
	{
		public string voice_id;
		public string name;
		public string category;
		public Dictionary<string, object> fine_tuning;
		public Dictionary<string, object> labels;
		public string description;
		public string preview_url;
	}

	[System.Serializable]
	public class VoiceList
	{
		public List<Voice> voices;
	}

	void ReadJSON(string input)
	{
		voiceList = JsonUtility.FromJson<VoiceList>(input);

		// foreach (var voice in voiceList.voices)
		// {
		// 	Debug.Log("Voice ID: " + voice.voice_id);
		// 	Debug.Log("Name: " + voice.name);
		// 	Debug.Log("Category: " + voice.category);
		// 	Debug.Log("Fine Tuning: " + voice.fine_tuning);
		// 	Debug.Log("Labels: " + voice.labels);
		// 	Debug.Log("Description: " + voice.description);
		// 	Debug.Log("Preview URL: " + voice.preview_url);
		// }
	}


	[Button]
	public void SynthesizeSpeech(string input)
	{
		StartCoroutine(SendSynthesizeRequest(input));
	}

	IEnumerator SendSynthesizeRequest(string input)
	{
		// Set up the request
		// TODO not random
		int rnd = Random.Range(0,voiceList.voices.Count);
		string url = apiUrl + voiceList.voices[rnd].voice_id;
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		request.SetRequestHeader("accept", "audio/mpeg");
		request.SetRequestHeader("xi-api-key", "11ce6c73cc5e0a806f38b60e31d7cae5");
		request.SetRequestHeader("Content-Type", "application/json");

		string model_id;
		// Set up the request data
		if (multiLingualVoice)
		{
			model_id = "eleven_multilingual_v1";
		}
		else
		{
			model_id = "eleven_monolingual_v1";
		}
		
		string jsonRequestBody = "{\"text\":\"" + input + "\",\"model_id\":\"" + model_id + "\",\"voice_settings\":{\"stability\":" + stability +
		                         ",\"similarity_boost\":" + similarity_boost + "}}";
		
		Debug.Log("Voice: JSON query = "+jsonRequestBody, gameObject);
		
		byte[] bodyRaw = new System.Text.UTF8Encoding(true).GetBytes(jsonRequestBody);
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerAudioClip("", AudioType.MPEG);

		// Send the request, or just fake it to not waste credit
		if (testMode)
			yield return new WaitForSeconds(1f);
		else
			yield return request.SendWebRequest();

		// Handle the response
		if (request.result == UnityWebRequest.Result.Success)
		{
			Debug.Log("Voice: Trying to say - '"+input+"'", gameObject);
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