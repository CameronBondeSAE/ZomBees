using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cam
{
	public class StoryEventManager : SerializedMonoBehaviour
	{
		public float time; // Day? Pm/Am? Night/Day?
		public List<StoryEvent> storyEvents;
	}
}