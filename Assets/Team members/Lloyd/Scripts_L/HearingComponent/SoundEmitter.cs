using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Lloyd
{
	[Serializable]
	public class SoundProperties
	{
		public SoundProperties(GameObject source, SoundEmitter.SoundType soundType, float radius, float distance, bool Directional, float fear, float beeness, Team team, int obstaclesBetween, string dialogue = "")
		{
			Source           = source;
			SoundType        = soundType;
			Radius           = radius;
			Distance         = distance;
			Directional      = false;
			Fear             = fear;
			Beeness          = beeness;
			Team             = team;
			ObstaclesBetween = obstaclesBetween;
			Dialogue         = dialogue;
		}

		public GameObject             Source;    //{ get; set; }
		public SoundEmitter.SoundType SoundType; //{ get; set; }
		public float                  Radius;    //{ get; set; }
		public float                  Distance;  //{ get; set; }
		public bool                   Directional = false;
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

		[SerializeField]
		int maxListeners = 20;

		IHear[] listeners;

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
			HalfBee,
			Bee,
			Environment,
			GunShot,
			BombExplosion,
			CreatureRepellant
		}

		public void EmitSound(SoundProperties soundProperties)
		{
			int numColliders;

			if (soundProperties.Directional)
			{
				Vector3 boxLocalSize = new Vector3(soundProperties.Radius, soundProperties.Radius, soundProperties.Distance);

				numColliders = Physics.OverlapBoxNonAlloc(gameObject.transform.position + transform.TransformDirection(0, 0, soundProperties.Distance / 2f), transform.TransformDirection(boxLocalSize), hitColliders);
			}
			else
			{
				numColliders = Physics.OverlapSphereNonAlloc(gameObject.transform.position, soundProperties.Radius, hitColliders);
			}

			for (int i = 0; i < numColliders; i++)
			{
				Collider collider = hitColliders[i];

				if (collider != null && collider.gameObject != soundProperties.Source)
				{
					listeners = collider.GetComponents<IHear>();

					foreach (var item in listeners)
					{
						if (item != null)
						{
							soundProperties.Source = gameObject;
							item.SoundHeard(soundProperties);
						}
					}
				}
			}
		}

		public SoundProperties testProperties;

		public void EmitTestSound()
		{
			testProperties.Source = gameObject;
			EmitSound(testProperties);
			//Debug.Log(testProperties);
		}
	}
}