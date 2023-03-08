using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Cam
{
	[Serializable] // Needed when a class ISN'T a monobehaviour to appear in inspector
	public class StoryEvent
	{
		// Time of event
		public float timeOfEvent;

		// Dependancies
		public List<ActionBase> dependencies;
		
		// Action
		public List<ActionBase> actions;
	}

}
