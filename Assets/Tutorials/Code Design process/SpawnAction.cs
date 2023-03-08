using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cam
{
	[Serializable]
	public class SpawnAction : ActionBase
	{
		public GameObject item;
		public Transform locationMarker;

		public List<GameObject> gos;
		
		public override void Activate()
		{
			base.Activate();
			
			Debug.Log("SpawnAction");
		}
	}
}