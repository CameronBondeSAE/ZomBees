using UnityEngine;

public class PlayerViewModel : MonoBehaviour
{
	public AudioClip deathClip;
	public AudioClip stepClip;
	
	public AudioSource audioSource;

	void OnEnable()
	{
	}

	void OnDisable()
	{
	}
	
	void StepSound()
	{
		audioSource.spatialBlend = 1f;
		audioSource.clip = stepClip;

		audioSource.Play();
		// soundEmitter.emit
	}
}
