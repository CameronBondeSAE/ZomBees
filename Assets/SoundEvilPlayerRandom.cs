using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvilPlayerRandom : MonoBehaviour
{
	public List<AudioClip> clips;
	public int             chanceOutOf10 = 7;

	public AudioSource audioSource;
	// Start is called before the first frame update
	void Start()
	{
		if (Random.Range(0, 10) > chanceOutOf10)
		{
			AudioClip audioClip = clips[Random.Range(0, clips.Count)];
			if (audioClip)
			{
				audioSource.clip = audioClip;
				audioSource.loop = true;
				audioSource.Play();
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
	}
}