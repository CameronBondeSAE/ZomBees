using Oscar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSack : DynamicObject, IItem
{
	GameObject   whoPickedMeUp;
	public float healthAmount = 50;

	public void Pickup(GameObject _whoPickedMeUp)
	{
		whoPickedMeUp                         = _whoPickedMeUp;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Collider>().enabled      = false;
	}


	public void Consume()
	{
		whoPickedMeUp.GetComponent<Health>().Change(healthAmount);
		Destroy(gameObject);
	}

	public void Dispose()
	{
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Collider>().enabled      = true;
	}

	public string Description()
	{
		return description;
	}
}