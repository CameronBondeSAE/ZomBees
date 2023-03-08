using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cam
{
	[Serializable]
	public class CivOutburstAction : ActionBase
	{
		public CivilGuy civilGuy;
		public string speech;
		
		public override void Activate()
		{
			base.Activate();
			
			Debug.Log("CivOutburstAction");
		}
	}
}