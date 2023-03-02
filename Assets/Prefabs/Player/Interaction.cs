using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cam
{
	public class Interaction : MonoBehaviour
	{
		void OnCollisionEnter(Collision collision)
		{
			// What we hit has a script that implements "ISwitchable" interface
			ISwitchable switchable = collision.gameObject.GetComponent<ISwitchable>();
		
			if (switchable != null)
			{
				if(Random.value>0.5f)
					switchable.TurnOn();
				else
					switchable.TurnOff();
			}
		}
	}
}
