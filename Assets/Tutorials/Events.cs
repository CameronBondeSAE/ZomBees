using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
	// This just defines a 'signature' for what the called functions should implement
	// So anyone subscribing to an event that uses this, needs to make function that conforms to this signature
	public delegate void DoEventTestDelegate(int aNumber, string aName);

	public event DoEventTestDelegate DoEventTest;


	// Using 'Action' class, which is just a pre-defined delegate
	public event Action<int, string> DoAnotherEventTest;

	// Start is called before the first frame update
	void Start()
	{
		NormalFunction(5);
		DoEventTest(5, "Cam!");
		DoAnotherEventTest(5, "Cam!");
	}

	public void NormalFunction(int someParameter)
	{
	}
}