using System;
using UnityEngine;

[Serializable]
public class Emotion
{
	public string description;
	[Range(0f, 1f)]
	public float strength;
}