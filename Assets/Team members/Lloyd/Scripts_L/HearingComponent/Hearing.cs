using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Sirenix.OdinInspector;
using UnityEngine;

public class Hearing : MonoBehaviour, IHear
{
	// Hearing Component uses IHear takes the gameObject Sound Emitter as source
	// calculates distance between HearingComp and source and fires a RaycastAll the length of distance at source
	// hitCount returns how many objects are in between HearingComp and source

	// Hearing Comp tracks number of sounds heard with soundsList
	// soundsList is sorted by input volume, loudest to the top
	// 

	[ReadOnly]
	public bool heardSound;

	[ReadOnly]
	public SoundProperties loudestRecentSound;

	[ReadOnly]
	public List<SoundProperties> soundsList = new List<SoundProperties>();

	private float soundDistance;

	private int thingsBetweenSound;

	public void Reset()
	{
		soundsList.Clear();
		heardSound         = false;
		loudestRecentSound = null;
	}
	

	public void Update()
	{
		if (soundsList.Count > 0)
		{
			heardSound         = true;
			loudestRecentSound = soundsList[0];
		}

		heardSound = false;
	}

	public void SoundHeard(SoundProperties soundProperties)
	{
		soundDistance = Vector3.Distance(transform.position, soundProperties.Source.transform.position);
		RaycastHit[] hits =
			Physics.RaycastAll(transform.position, soundProperties.Source.transform.position - transform.position, soundDistance);
		thingsBetweenSound = hits.Length;

		soundProperties.ObstaclesBetween = thingsBetweenSound;
		soundProperties.Distance         = soundDistance;
		soundsList.Add(soundProperties);

		// TODO 1-distance * volume should be the metric
		soundsList.Sort(Comparison);
		OnSoundHeardEvent(soundProperties);
	}

	int Comparison(SoundProperties a, SoundProperties b)
	{
		// CHECK: Does this sort correctly?
		int compareTo = (a.Radius / a.Distance).CompareTo(b.Radius / b.Distance);
		
		return compareTo;
	}

	public event Action<SoundProperties> SoundHeardEvent;

	public void OnSoundHeardEvent(SoundProperties soundProperties)
	{
		SoundHeardEvent?.Invoke(soundProperties);

		Debug.Log("Heard something" + soundProperties.Distance + " far away");
		Debug.Log(+thingsBetweenSound + " number of objects between");
		Debug.Log("Scary level :" + soundProperties.Fear);
		Debug.Log("Bee level :" + soundProperties.Beeness);
	}


	//where to put perlin randomiser for sound?
	// float noiseX = Mathf.PerlinNoise(point.x * scale, 0f);
	// float noiseY = Mathf.PerlinNoise(point.y * scale, 0f);
	// float noiseZ = Mathf.PerlinNoise(point.z * scale, 0f);
	//
	// // Add the Perlin noise values to each component of the point
	// point.x += noiseX;
	// point.y += noiseY;
	// point.z += noiseZ;
	//public float perlionScale = 1f;
}