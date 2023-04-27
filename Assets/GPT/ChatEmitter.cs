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


	public void Emit(string input, GameObject source)
	{
		TraitStats trait = civilianTraits.GetTrait(beeNess_traitScriptableObject);

		if (trait != null)
		{
			float beeness = trait.value;

			Debug.Log("Beeness = " + beeness);
			soundEmitter.EmitSound(new SoundProperties(source, SoundEmitter.SoundType.CivTalk, talkRadius, talkDistance, directional, 0,
													   beeness, Team.Human, 0, input));
		}
	}
}