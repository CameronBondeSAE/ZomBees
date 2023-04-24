using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
// using Unity.Netcode;
using UnityEngine;


public class Test : SerializedMonoBehaviour
{
	public CivilianTraits civilianTraits;

	public string thing;

	public bool panicked;

	public Stat<float> amountOfGoo = new Stat<float>();
	public Stat<int>   anxiety     = new Stat<int>();


	public Dictionary<string, Stat<float>> testStatsDic;

	// public NetworkVariable<float> health;


	public Stat<float> beeness;

	public float Beeness
	{
		get => beeness.Value;
		set
		{
			if (value > 100)
				Debug.LogError("STOP IT ARTIST");
			beeness.Value =  value;
			anxiety.Value += (int) (beeness.Value / 10f);
			// Fire event
		}
	}

	public void DoThing(string _thing)
	{
		Debug.Log(_thing);
	}

	// Start is called before the first frame update
	void Start()
	{
		anxiety.Value = 5;
	}


	public TraitScriptableObject fear;

	// Update is called once per frame
	void Update()
	{
		if (civilianTraits != null)
		{
			Debug.Log("Fear Dick = " + civilianTraits.GetTrait(fear).value);
			
			// Debug.Log("Fear Dick = " + civilianTraits.traitsDictionary[fear].value);
		}
	}
}