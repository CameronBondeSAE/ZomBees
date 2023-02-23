using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cam
{
	public class LightEnable : MonoBehaviour, ISwitchable
	{
		// public Switch camSwitch;
		//
		// // Start is called before the first frame update
		// void Start()
		// {
		// 	camSwitch.SwitchEvent += CamSwitchOnSwitchEvent;
		// }

		public void CamSwitchOnSwitchEvent(object sender, EventArgs args)
		{
			Debug.Log("GO");
		}

		// Update is called once per frame
		void Update()
		{
        
		}
		
		public void TurnOn()
		{
			Debug.Log("OOOONN!!");
		}

		public void TurnOff()
		{
			throw new System.NotImplementedException();
		}

	}
}
