using Oscar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Marcus
{
	[RequireComponent(typeof(PatrolPoint))]
	public class FlyDudeSpawner : MonoBehaviour
	{
		public GameObject  flyDude;
		public int         amount;
		public PatrolPoint homeBase;

		/// <summary>
		/// Number of seconds between each ai is spawned
		/// </summary>
		public float spawnDelay;

		private float            spawnTimer;
		public  List<GameObject> spawnedAI;

		void Awake()
		{
			if (homeBase == null)
			{
				homeBase = GetComponent<PatrolPoint>();
			}
		}

		void Update()
		{
			spawnTimer -= Time.deltaTime;

			if (spawnTimer <= 0 && spawnedAI.Count < amount)
			{
				Spawn();
				spawnTimer = spawnDelay;
			}
		}

		public void Spawn()
		{
			GameObject ai = Instantiate(flyDude, transform.position,
										Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), 0));
			spawnedAI.Add(ai);
			ai.GetComponent<DynamicObject>().homeBase = homeBase;
		}
	}
}