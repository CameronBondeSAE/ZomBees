using System;
using System.Collections.Generic;
using KevinCastejon.ConeMesh;
using Oscar;
using System.Collections;
using UnityEngine;

public class OscarVision : MonoBehaviour
{
	#region Variables

	public List<GameObject> beesInSight;

	public List<GameObject> foodInSight;

	public List<GameObject> civsInSight;

	public List<GameObject> objectsInSight;

	public List<GameObject> lightInSight;


	public List<DynamicObject> allInSight;

	public delegate void OnObjectSeen(GameObject thing);

	public event OnObjectSeen objectSeenEvent;

	#endregion

	void Start()
	{
		StartCoroutine(CheckStillVisible());
	}

	#region OnTriggerEnter

	private void OnTriggerEnter(Collider other)
	{
		//everything vision for anyone's use :D
		if (other != null)
		{
			if (other.GetComponent<DynamicObject>() != null)
			{
				DynamicObject dynamicObj = other.GetComponent<DynamicObject>();

				allInSight.Add(dynamicObj);
			}
		}
	}

	#endregion

	#region OnTriggerStay

	private IEnumerator CheckStillVisible()
	{
		while (true)
		{
			beesInSight.Clear();
			// CLEAR ALL OTHERS

			foreach (DynamicObject dynamicObj in allInSight)
			{
				//everything vision for anyone's use :D
				if (dynamicObj != null)
				{
					// TODO: LINECAST HERE. If false, continue (next in list)
					
					
					//are they a Bee, use this:
					if (dynamicObj.isBee == true)
					{
						GameObject beeStuff = other.gameObject;
						// TODO: Add to list here
						
						
						objectSeenEvent?.Invoke(beeStuff);
					}

					//are they a Civ, use this:
					if (dynamicObj.isBee == false)
					{
						GameObject civStuff = other.gameObject;
						// TODO: Add to list here

						objectSeenEvent?.Invoke(civStuff);
					}

					//is it a food, use this:
					if (dynamicObj.isFood == true)
					{
						GameObject foodStuff = other.gameObject;
						// TODO: Add to list here

						objectSeenEvent?.Invoke(foodStuff);
					}

					//is it a Object, use this:
					if (dynamicObj.isObject == true)
					{
						GameObject objectStuff = other.gameObject;
						// TODO: Add to list here

						objectSeenEvent?.Invoke(objectStuff);
					}

					//is lit up, use this:
					if (dynamicObj.isLit == true)
					{
						GameObject litObj = other.gameObject;
						// TODO: Add to list here

						objectSeenEvent?.Invoke(litObj);
					}
				}
			}

			yield return new WaitForSeconds(3f); // TODO: Variable
		}
	}

	#endregion

	#region OnTriggerExit

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<DynamicObject>() != null)
		{
			DynamicObject dynamicObj = other.GetComponent<DynamicObject>();

			allInSight.Remove(dynamicObj);
		}
	}

	#endregion
}