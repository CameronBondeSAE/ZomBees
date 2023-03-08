using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cam
{
	
	[Serializable]
	public class ActionBase : MonoBehaviour
	{
		// Human readable purely for the inspector for StoryTimeManager
		public string description;

		// Success marker
		public bool success;

		// Customise this for each action
		
		[Button]
		public virtual void Activate()
		{
			
		}
	}

}