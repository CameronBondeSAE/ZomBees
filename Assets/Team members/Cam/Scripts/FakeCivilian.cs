using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCivilian : MonoBehaviour, IInteractable
{
	public string GPTInfo;
	public string mainPrompt;

	public CivGPT civGpt;

	public void ActivateChatInterface()
	{
		
	}
	
	public void Interact()
	{
		
	}

	public void Inspect()
	{
		throw new System.NotImplementedException();
	}
}
