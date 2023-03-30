using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombees/EmotionalCore", order = 1)]
public class EmotionalCore : ScriptableObject
{
	[SerializeField]
	public List<Emotion> _emotions;
}


