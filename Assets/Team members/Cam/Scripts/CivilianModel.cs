using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class CivilianModel : DynamicObject, IInteractable
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
