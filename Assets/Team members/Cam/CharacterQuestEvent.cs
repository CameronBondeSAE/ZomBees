using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "Zombees/Cams test - StoryEventActionTest", order = 1)]
[Serializable]
public class CharacterQuestEvent
{
	public string descriptionForGPT;
	public CivGPT.CivAction CivAction;
	public int TimeToPerformAction;
	public string GridCoordinateForAction;
	public CivGPT.GPTResponseData gptResponseData;
}