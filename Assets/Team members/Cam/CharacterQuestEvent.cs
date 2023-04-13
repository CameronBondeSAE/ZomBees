using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ZomBees/Cams test - StoryEventActionTest", order = 1)]

public class CharacterQuestEvent : ScriptableObject
{
    public float       time;
    public string      descriptionForGPT;
}
