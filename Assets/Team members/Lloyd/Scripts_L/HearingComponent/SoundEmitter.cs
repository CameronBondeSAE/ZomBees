using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Team_members.Lloyd.Scripts_L.HearingComponent
{
	[Serializable]
	public class SoundProperties
	{
		public SoundProperties(GameObject source, SoundEmitter.SoundType soundType, float radius, float distance, float fear, float beeness, Team team, int obstaclesBetween, string dialogue = "")
		{
			Source           = source;
			SoundType        = soundType;
			Radius           = radius;
			Distance         = distance;
			Fear             = fear;
			Beeness          = beeness;
			Team             = team;
			ObstaclesBetween = obstaclesBetween;
			Dialogue         = dialogue;
		}

		public GameObject             Source;           //{ get; set; }
		public SoundEmitter.SoundType SoundType;        //{ get; set; }
		public float                  Radius;           //{ get; set; }
		public float                  Distance;         //{ get; set; }
		public float                  Fear;             //{ get; set; }
		public float                  Beeness;          //{ get; set; }
		public Team                   Team;             //{ get; set; }
		public int                    ObstaclesBetween; //{ get; set; }
		public string                 Dialogue;
	}

	public class SoundEmitter : SerializedMonoBehaviour //MonoBehaviour
	{
		// sound emitter uses EmitSound to create a nonAllocSphere as big as radius
		// the sphere detects everything within it with IHear and tells them they heard the sound
		// EmitSound transmits the gameObject.transform.position of itself as origin
		// EmitSound transmits the float fear for how much fear level is changed
		// EmitSound transmits the float team to communicate what team origin is (Human / Bee)
		Collider[] hitColliders;

		[SerializeField] int maxListeners = 20;

		IHear listener;

		void Awake()
		{
			hitColliders = new Collider[maxListeners];
		}

		// enum :)
		// ordered in whatever of importance
		public enum SoundType
		{
			Unknown,
			PlayerTalk,
			CivTalk,
			Bee,
			Environment,
			GunShot,
			BombExplosion,
			CreatureRepellant
		}

		public void EmitSound(SoundProperties soundProperties)
		{
			int numColliders =
				Physics.OverlapSphereNonAlloc(gameObject.transform.position, soundProperties.Radius, hitColliders);

			for (int i = 0; i < numColliders; i++)
			{
				Collider collider = hitColliders[i];

				listener = collider.GetComponent<IHear>();
				if (listener != null)
				{
					soundProperties.Source = gameObject;
					listener.SoundHeard(soundProperties);
					// listenerList.Add(listener);
				}
			}
		}

		public SoundProperties testProperties;

		public void EmitTestSound()
		{
			testProperties.Source = gameObject;
			EmitSound(testProperties);
			Debug.Log(testProperties);
		}
	}
}