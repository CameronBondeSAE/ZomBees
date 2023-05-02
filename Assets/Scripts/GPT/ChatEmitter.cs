using Lloyd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatEmitter : MonoBehaviour
{
	[SerializeField]
	private float talkRadius = 1f;

	public bool directional = false;

	[SerializeField]
	private float talkDistance = 5f;

	public SoundEmitter soundEmitter;

	public CivilianTraits civilianTraits;

	public TraitScriptableObject beeNess_traitScriptableObject;

	float beeness;

	// eg So characters can look at each other while typing/thinking
	public void PrepareEmit()
	{
		TraitStats trait = civilianTraits.GetTrait(beeNess_traitScriptableObject);

		if (trait != null)
		{
			beeness = trait.value;
			// Get the character's attention
			soundEmitter.EmitSound(new SoundProperties(gameObject, SoundEmitter.SoundType.CivTalk, talkRadius,
				talkDistance, directional, 0, beeness, Team.Human, 0, ""));
		}
	}

	// You must call PrepareEmit first
	public void Emit(string input, GameObject source)
	{
		Debug.Log("Beeness = " + beeness);
		soundEmitter.EmitSound(new SoundProperties(source, SoundEmitter.SoundType.CivTalk, talkRadius, talkDistance,
			directional, 0,
			beeness, Team.Human, 0, input));
	}
}