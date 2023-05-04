using System;
using System.Collections.Generic;
using KevinCastejon.ConeMesh;
using Oscar;
using System.Collections;
using UnityEngine;

public class OscarVision : MonoBehaviour
{
	#region Variables

	public float sightRefreshTime = 2f;
	
	public List<DynamicObject> beesInSight = new List<DynamicObject>();	

	public List<DynamicObject> foodInSight = new List<DynamicObject>();

	public List<DynamicObject> civsInSight = new List<DynamicObject>();

	public List<DynamicObject> objectsInSight = new List<DynamicObject>();

	public List<DynamicObject> lightInSight = new List<DynamicObject>();
	
	public List<DynamicObject> allInSight = new List<DynamicObject>();

	public delegate void OnObjectSeen(GameObject thing);

	public event OnObjectSeen objectSeenEvent;

	#endregion

	void Start()
	{
		allInSight  = new List<DynamicObject>();
		beesInSight = new List<DynamicObject>();
		civsInSight = new List<DynamicObject>();
		foodInSight = new List<DynamicObject>();
		lightInSight = new List<DynamicObject>();
		objectsInSight = new List<DynamicObject>();		
		
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
				
				if (!allInSight.Contains(dynamicObj))
				{
					allInSight.Add(dynamicObj);
					
					allInSight.Sort(Comparison);
				}
			}
		}
	}

	int Comparison(DynamicObject x, DynamicObject y)
	{
		if (Vector3.Distance(transform.position, x.transform.position) < Vector3.Distance(transform.position, y.transform.position))
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	#endregion

	#region OnTriggerStay

	private IEnumerator CheckStillVisible()
	{
		while (true)
		{			
			// CLEAR ALL OTHERS
			beesInSight.Clear();
			civsInSight.Clear();
			foodInSight.Clear();
			lightInSight.Clear();
			objectsInSight.Clear();

			foreach (DynamicObject dynamicObj in allInSight)
			{
				//everything vision for anyone's use :D
				if (dynamicObj != null)
				{
					//LINECAST HERE. If false, continue (next in list)
					// Perform linecast
					bool hit = Physics.Linecast(transform.position, dynamicObj.transform.position);

					if (hit)
					{
						//are they a Bee, use this:
						if (!beesInSight.Contains(dynamicObj))
						{
							if (dynamicObj.isBee == true)
		                    {
                     			//Add to list here
		                        beesInSight.Add(dynamicObj);
		                    }
						}
						if (!civsInSight.Contains(dynamicObj))
						{
							//are they a Civ, use this:
							if (dynamicObj.isCiv == true)
							{
								//Add to list here
								civsInSight.Add(dynamicObj);
							}
						}
						if (!foodInSight.Contains(dynamicObj))
						{
							//is it a food, use this:
							if (dynamicObj.isFood == true)
							{
								//Add to list here
								foodInSight.Add(dynamicObj);
							}
						}
						if (!objectsInSight.Contains(dynamicObj))
						{
							//is it a Object, use this:
							if (dynamicObj.isObject == true)
							{
								//Add to list here
								objectsInSight.Add(dynamicObj);
							}
						}
						if (!lightInSight.Contains(dynamicObj))
						{
							//is lit up, use this:
							if (dynamicObj.isLit == true)
							{
								//Add to list here
								lightInSight.Add(dynamicObj);
							}
						}
						// Invoke objectSeenEvent
						objectSeenEvent?.Invoke(dynamicObj.gameObject);
					}
				}
			}

			yield return new WaitForSeconds(sightRefreshTime);
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
			
			// print(dynamicObj.description);
			
		}
	}

	#endregion
}