using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ToggleStateTest : MonoBehaviour, ISwitchable
{
	public GeneratorCamStates generatorCamStates;
	
	public void ToggleGenerator()
	{
		// generatorCamStates.ChangeState(on);
		
		// Hack. Toggle
		generatorCamStates.isGeneratorOn = !generatorCamStates.isGeneratorOn;
	}

	public void TurnOn()
	{
		
	}

	public void TurnOff()
	{
		
	}

	public void Toggle()
	{
		throw new System.NotImplementedException();
	}
}
