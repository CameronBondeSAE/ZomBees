using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Zombees/Traits list", order = 2)]

public class TraitsListScriptableObject : ScriptableObject
{
	public List<TraitScriptableObject> traits;
}
