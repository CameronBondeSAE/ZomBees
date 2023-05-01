using System;
using Lloyd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used on the player to interface with the InputField typing of text
/// </summary>
public class ChatBox : MonoBehaviour
{
	public TMP_InputField tmpInputField;

	public GameObject view;

	public ChatEmitter chatEmitter;

	public GameObject source;
	
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
		Deactivate();
	}

	public void Activate()
	{
		view.SetActive(true);
		tmpInputField.ActivateInputField();
		chatEmitter.PrepareEmit();
	}

	public void Deactivate()
	{
		tmpInputField.DeactivateInputField();
		view.SetActive(false);
	}


}