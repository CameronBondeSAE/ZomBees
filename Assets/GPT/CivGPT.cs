using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using Lloyd;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class CivGPTEventArgs : EventArgs
{
}

public class CivGPT : MonoBehaviour, IHear
{
	public string apiKeys;

	OpenAIAPI api;

	[TextArea(5, 40)]
	public string systemMessage;

	[TextArea(5, 40)]
	public string prompt;

	// TODO one per Civ pair
	public OpenAI_API.Chat.Conversation chat;
	bool                                startedChat = false;

	public QuestTrackerSimple questTrackerSimple;

	public CivilianTraits civilianTraits;

	public RandomNavmeshTest randomNavmeshTest;

	public event EventHandler<string>    GPTOutputDialogueEvent;
	public event EventHandler<CivAction> GPTPerformingActionEvent;


	public class TestEventArgs : EventArgs
	{
		public CivAction civAction;
		public string    thing;
		public bool      yes;
	}

	public event EventHandler<TestEventArgs> TestEventWithArgs;


	/// <summary>
	/// HACK SECTION
	/// </summary>

	// View
	[FormerlySerializedAs("civilian")]
	[FormerlySerializedAs("fakeCivilian")]
	public CivilianModel civilianModel;

	[FormerlySerializedAs("simpleShoot")]
	public Pistol pistol; // HACK

	public Transform gun;

	public Transform        head;
	public List<GameObject> decals;

	[SerializeField]
	private float gunForce = 10f;

	void Awake()
	{
		Init();


		// HACK
		// if (gun != null)
		// {
		// gun.GetComponent<Rigidbody>().isKinematic = true;
		// gun.GetComponent<Collider>().enabled = false;
		// }

		// foreach (var decal in decals)
		// {
		// decal.SetActive(false);
		// }
	}

	private void Init()
	{
		// NOTE: This is Cam's API key. Don't abuse it. Put your own in if you like.
		// apiKeys = "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";
		api = new OpenAI_API.OpenAIAPI(apiKeys);

		questTrackerSimple.QuestEventStarted += QuestTrackerSimpleOnquestEventStarted;
	}

	void QuestTrackerSimpleOnquestEventStarted(string input)
	{
		AppendUserInput(input);
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
		chat.Model                         = Model.ChatGPTTurbo;
		chat.RequestParameters.MaxTokens   = 500;
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
		// Demo JSON

		// string json = @"{
		//           ""emotion"": ""Angry"",
		//           ""outputSpeech"": ""Insane? No, I'm perfectly sane. These fucking bee creatures are everywhere! I'm not gonna sit here and wait to be stung to death!"",
		//           ""action"": ""Shoot"",
		//           ""importance"": 1,
		//           ""fear"": 0.8
		//       }";

		// JSONToReal(json);

		// return;

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


		// HACK: Demo suicide sequence
		// Sequence sequence = DOTween.Sequence();
		// sequence.AppendInterval(7.7f);
		// sequence.Append(gun.transform.DORotate(new Vector3(-109.594f, 0, 0), 0.3f, RotateMode.LocalAxisAdd)
		// 	.SetEase(Ease.InOutQuad));
		// sequence.Play();

		MyObject myObject = JsonConvert.DeserializeObject<MyObject>(json);

		GPTOutputDialogueEvent?.Invoke(this, myObject.OutputSpeech);

		// Wait for action to happen after talking
		await Task.Delay(myObject.OutputSpeech.Length * 100);


		GPTPerformingActionEvent?.Invoke(this, myObject.CivAction);

		TestEventWithArgs?.Invoke(this, new TestEventArgs
										{
											civAction = CivAction.Shoot,
											thing     = "THIS",
											yes       = false
										});


		switch (myObject.CivAction)
		{
			case CivAction.Shoot:
				pistol.Shoot();
				break;
			case CivAction.GatherFood:
				// Check memories for food, else go to resource points


				break;
			case CivAction.FollowPlayer:
				break;
			case CivAction.RunAway:
				randomNavmeshTest.FindRandomSpot();
				break;
			case CivAction.FrozenWithFear:
				break;
			case CivAction.DoNothing:
				break;
			case CivAction.CommitSuicide:
				pistol.Shoot();

				// HACK move to model for general death
				gun.transform.parent                      = null;
				gun.GetComponent<Rigidbody>().isKinematic = false;
				gun.GetComponent<Collider>().enabled      = true;

				GetComponent<NavMeshAgent>().enabled  = false;
				GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				foreach (var decal in decals)
				{
					decal.SetActive(true);
				}

				// bloodParticles.Emit(150);

				// HACK move to gun itself
				head.GetComponent<Rigidbody>()
					.AddForceAtPosition(transform.InverseTransformVector(new Vector3(-30f, 50f, -150f)) * gunForce,
										transform.position + new Vector3(0, 0, 0));

				break;
			case CivAction.Shout:
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
		public CivAction CivAction { get; set; }

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
		Curious,
		Neutral
	}

	public enum CivAction
	{
		Shoot,
		GatherFood,
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

	public void SoundHeard(SoundProperties soundProperties)
	{
		if (soundProperties.SoundType == SoundEmitter.SoundType.CivTalk)
		{
			AppendUserInput(soundProperties.Dialogue);
		}
	}
}