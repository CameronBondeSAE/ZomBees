using Oscar;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

namespace CameronBonde
{


	public class Torch : DynamicObject, IItem, IInteractable
	{
		public GameObject lightCone;

		public void Consume()
		{

		}

		public void Dispose()
		{
		}

		public string Description()
		{
			return description;
		}

		public void Pickup(GameObject whoPickedMeUp)
		{
		}

		public void Interact()
		{
			lightCone.SetActive(!lightCone.activeSelf);
		}

		public void Inspect()
		{
		}
	}

}