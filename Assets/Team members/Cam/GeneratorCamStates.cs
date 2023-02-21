using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCamStates : MonoBehaviour
{
	public float currentPowerLevel;
	public float maxPowerLevel;
	public float powerGenerationRate;
	public bool  isGeneratorOn = true;
	bool         lastIsGeneratorOn;


	void Update()
	{
		if (isGeneratorOn != lastIsGeneratorOn)
		{
			if (isGeneratorOn && currentPowerLevel < maxPowerLevel)
			{
				Debug.Log("Gen on");
				// audio.Play(running);
				currentPowerLevel += powerGenerationRate * Time.deltaTime;
				if (currentPowerLevel > maxPowerLevel)
				{
					currentPowerLevel = maxPowerLevel;
				}
			}

			if (isGeneratorOn == false)
			{
				Debug.Log("Gen off");
				// audio.Stop(running);
			}
		}

		lastIsGeneratorOn = isGeneratorOn;
	}
}