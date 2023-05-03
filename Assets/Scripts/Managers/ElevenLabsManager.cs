using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ElevenLabsManager : MonoBehaviour
{
	public static ElevenLabsManager Instance;

	public bool ElevenLabsVoice = false;

	public string apiUrl = "https://api.elevenlabs.io/v1/text-to-speech/";
	private string apiKey = "6fee898400ed59fa0e53d71ed9e585e0";

	public VoiceList voiceList;

	private void Awake()
	{
		Instance = this;

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


	[System.Serializable]
	public class GPTResponse
	{
		public string voice_id { get; set; }
		public string name { get; set; }
		public string category { get; set; }
		public string description { get; set; }
		public string preview_url { get; set; }
	}

	public List<GPTResponse> GptResponses;

	public string test;

	[Button]
	public void TestJSON()
	{
		GptResponses = JsonConvert.DeserializeObject<List<GPTResponse>>(test);
	}
}