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
		StartCoroutine(SendSynthesizeRequest(textToSynthesize));
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