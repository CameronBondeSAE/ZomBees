using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ElevenLabsVoiceAPITest : MonoBehaviour
{
	public AudioSource audioSource;

	public int stability = 0;
	public int similarity_boost = 0;
	[SerializeField]
	private bool testMode = false;

	[SerializeField]
	public bool multiLingualVoice = true;

	int voiceIndex;

	void Start()
	{
		voiceIndex = Random.Range(0, ElevenLabsManager.Instance.voiceList.voices.Count);
	}

	[Button]
	public void PlayTheLastVoiceReceived()
	{
		audioSource.Play();
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
		string          url     = ElevenLabsManager.Instance.apiUrl + ElevenLabsManager.Instance.voiceList.voices[voiceIndex].voice_id;
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

		input = Utilities.RemoveCarriageReturns(input);
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