using System;
using Lloyd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
	[SerializeField]
	private float talkRadius = 1f;
	
	[SerializeField] private float talkDistance = 5f;

	public SoundEmitter soundEmitter;

	public CivilianTraits civilianTraits;

	public TraitScriptableObject beeNess_traitScriptableObject;

	public TMP_InputField tmpInputField;

	public GameObject view;

	private void Awake()
	{
		tmpInputField.onSubmit.AddListener(NewInput);
		tmpInputField.text = "";
		// tmpInputField.ActivateInputField();
		Deactivate();
	}

	public void Activate()
	{
		view.SetActive(true);
		tmpInputField.ActivateInputField();
	}

	public void Deactivate()
	{
		tmpInputField.DeactivateInputField();
		view.SetActive(false);
	}

	public void NewInput(string input)
	{
		TraitStats trait = civilianTraits.GetTrait(beeNess_traitScriptableObject);

		if (trait != null)
		{
			float beeness = trait.value;

			Debug.Log("Beeness = "+beeness);
			soundEmitter.EmitSound(new SoundProperties(gameObject, SoundEmitter.SoundType.CivTalk, talkRadius, talkDistance, true, 0,
				beeness, Team.Human, 0, input));
		}

		// Turn myself off
		// tmpInputField.text = "";
		Deactivate();
		// tmpInputField.ActivateInputField();
		// tmpInputField.DeactivateInputField();

		// RaycastHit hitInfo;
		// if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo, 3f, Int32.MaxValue,
		// 	    QueryTriggerInteraction.Ignore))
		// {
		// 	testGpt.AppendUserInput(input, hitInfo.transform.GetComponent<FakeCivilian>());
		// }
	}
}