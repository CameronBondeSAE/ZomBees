using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CivGPT : MonoBehaviour
{
	public string apiKeys;

	OpenAIAPI api;

	[TextArea(5, 40)]
	public string systemMessage;

	[TextArea(5, 40)]
	public string prompt;

	OpenAI_API.Chat.Conversation chat;

	bool startedChat = false;
	public QuestTrackerSimple questTrackerSimple;

	public CivilianTraits civilianTraits;

	// View
	public AudioClip clip;
	public TextMeshPro textMeshProUGUI;
	public FakeCivilian fakeCivilian;
	public SimpleShoot simpleShoot; // HACK
	
	public Transform gun;

	public Transform head;
	public List<GameObject> decals;
	[SerializeField]
	private float gunForce = 10f;

	[SerializeField]
	private ParticleSystem bloodParticles;
	TextMeshPro textMeshPro;

	void Awake()
	{
		Init();
		
		// HACK
		textMeshPro = fakeCivilian.transform.GetComponentInChildren<TextMeshPro>();
		GetComponent<AudioSource>().clip = clip;

		if (gun != null)
		{
			gun.GetComponent<Rigidbody>().isKinematic = true;
			gun.GetComponent<Collider>().enabled = false;
		}

		foreach (var decal in decals)
		{
			decal.SetActive(false);
		}

	}

	private void Init()
	{
		// NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
		// apiKeys = "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";
		api = new OpenAI_API.OpenAIAPI(apiKeys);

		questTrackerSimple.QuestEventStarted += QuestTrackerSimpleOnquestEventStarted;
	}

	void QuestTrackerSimpleOnquestEventStarted(string obj)
	{
		AppendUserInput(obj);
	}

	[Button]
	public void StartChatConversation()
	{
		startedChat = true;
		if (api == null)
		{
			Init();
		}

		chat = api.Chat.CreateConversation();

		/// give instruction as System
		chat.Model = Model.ChatGPTTurbo;
		chat.RequestParameters.MaxTokens = 500;
		chat.RequestParameters.Temperature = 0.9f; // Creative

		chat.AppendSystemMessage(systemMessage);

		// give a few examples as user and assistant
		// chat.AppendUserInput("Is this an animal? Cat");
		// chat.AppendExampleChatbotOutput("Yes");
		// chat.AppendUserInput("Is this an animal? House");
		// chat.AppendExampleChatbotOutput("No");
	}

	[Button]
	public async void AppendUserInput(string input)
	{
		string json = @"{
            ""emotion"": ""Angry"",
            ""outputSpeech"": ""Insane? No, I'm perfectly sane. These fucking bee creatures are everywhere! I'm not gonna sit here and wait to be stung to death!"",
            ""action"": ""Shoot"",
            ""importance"": 1,
            ""fear"": 0.8
        }";

		JSONToReal(json);

		return;

		if (startedChat == false)
			StartChatConversation();

		chat.AppendUserInput(input);


		var res = await chat.GetResponseFromChatbotAsync();

		Debug.Log(res);

		JSONToReal(res);

		// and get the response
		// await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
		// {
		// 	Debug.Log(res);
		// 	fakeCivilian.transform.GetComponentInChildren<TextMeshPro>().text += res;
		// }
	}

	public async void JSONToReal(string json)
	{
		// To deserialize the JSON into a MyObject instance:
		// string json = @"{
		//     ""emotion"": ""Scared"",
		//     ""outputSpeech"": ""I'll see what I can do, but you gotta understand, I may not come back from this. These bees are relentless and they're everywhere. If I make it back, I'm gonna need a way out of here fast. Wish me luck."",
		//     ""action"": ""RetrieveItem"",
		//     ""importance"": 0.8,
		//     ""fear"": 0.9
		// }";

		// textMeshPro.text = myObject.OutputSpeech; // HACK
		// GetComponent<AudioSource>().clip = clip;
		GetComponent<AudioSource>().Play();

		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(7.7f);
		sequence.Append(gun.transform.DORotate(new Vector3(-109.594f, 0, 0), 0.3f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad));
		sequence.Play();

		await Task.Delay(8000);
		// Wait for action to happen

		MyObject myObject = JsonConvert.DeserializeObject<MyObject>(json);

		switch (myObject.Action)
		{
			case Action.Shoot:
				textMeshPro.enabled = false;
				simpleShoot.Shoot();

				gun.transform.parent = null;
				gun.GetComponent<Rigidbody>().isKinematic = false;
				gun.GetComponent<Collider>().enabled = true;

				GetComponent<NavMeshAgent>().enabled = false;
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				head.GetComponent<Rigidbody>()
					.AddForceAtPosition(transform.InverseTransformVector(new Vector3(-30f, 50f, -150f)) * gunForce,
						transform.position + new Vector3(0, 0, 0));

				foreach (var decal in decals)
				{
					decal.SetActive(true);
				}

				bloodParticles.Emit(150);

				break;
			case Action.RetrieveItem:
				break;
			case Action.FollowPlayer:
				break;
			case Action.RunAway:
				break;
			case Action.FrozenWithFear:
				break;
			case Action.DoNothing:
				break;
			case Action.CommitSuicide:
				break;
			case Action.Shout:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}


	public class MyObject
	{
		[JsonProperty("emotion")]
		public Emotion Emotion { get; set; }

		[JsonProperty("outputSpeech")]
		public string OutputSpeech { get; set; }

		[JsonProperty("action")]
		public Action Action { get; set; }

		[JsonProperty("importance")]
		public double Importance { get; set; }

		[JsonProperty("fear")]
		public double Fear { get; set; }
	}

	public enum Emotion
	{
		Angry,
		Scared,
		Happy,
		Curious
	}

	public enum Action
	{
		Shoot,
		RetrieveItem,
		FollowPlayer,
		RunAway,
		FrozenWithFear,
		DoNothing,
		CommitSuicide,
		Shout
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