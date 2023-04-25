using System;
using Lloyd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
	[SerializeField]
	private float talkRadius = 5f;

	public SoundEmitter soundEmitter;

	public CivilianTraits civilianTraits;

	public TraitScriptableObject beeNess_traitScriptableObject;

	public TMP_InputField tmpInputField;

	private void Awake()
	{
		tmpInputField.onEndEdit.AddListener(NewInput);
		tmpInputField.text = "";
		tmpInputField.ActivateInputField();
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void NewInput(string input)
	{
		TraitStats trait = civilianTraits.GetTrait(beeNess_traitScriptableObject);

		if (trait != null)
		{
			float beeness = trait.value;
			soundEmitter.EmitSound(new SoundProperties(gameObject, SoundEmitter.SoundType.CivTalk, talkRadius, 0, 0,
				beeness, Team.Human, 0, input));
		}
		
		// Turn myself off
		// gameObject.SetActive(false);
		tmpInputField.text = "";
		tmpInputField.ActivateInputField();
		// tmpInputField.DeactivateInputField();

		// RaycastHit hitInfo;
		// if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, 3f, Int32.MaxValue,
		// 	    QueryTriggerInteraction.Ignore))
		// {
		// 	testGpt.AppendUserInput(input, hitInfo.transform.GetComponent<FakeCivilian>());
		// }
	}
}