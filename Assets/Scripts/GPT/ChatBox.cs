using System;
using DG.Tweening;
using Lloyd;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Used on the player to interface with the InputField typing of text
/// </summary>
public class ChatBox : MonoBehaviour
{
	public TMP_InputField tmpInputField;

	public TextMeshProUGUI playerText;

	public GameObject inputFieldView;

	public ChatEmitter chatEmitter;

	public GameObject source;

	Sequence sequence;

	private void Awake()
	{
		tmpInputField.onSubmit.AddListener(EmitChat);
		tmpInputField.text = "";
		// tmpInputField.ActivateInputField();
		Deactivate();
	}

	void EmitChat(string input)
	{
		chatEmitter.directional = true;
		chatEmitter.Emit(input, source);
		playerText.text = input;

		// Previous still running seq
		sequence.Complete();
		
		sequence = DOTween.Sequence();
		sequence.Append(playerText.DOFade(1.0f, 0.25f));
		sequence.AppendInterval(1f);
		sequence.Append(playerText.DOFade(0, 2f));
		sequence.Insert(0, playerText.rectTransform.DOMoveY(0f, 0f));
		sequence.Insert(0, playerText.rectTransform.DOMoveY(85f, 3f));
		sequence.Play();
		
		Deactivate();
	}

	public void Activate()
	{
		inputFieldView.SetActive(true);
		tmpInputField.ActivateInputField();
		chatEmitter.PrepareEmit();
	}

	public void Deactivate()
	{
		tmpInputField.DeactivateInputField();
		inputFieldView.SetActive(false);
	}


}