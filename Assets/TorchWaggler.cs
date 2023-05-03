using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TorchWaggler : MonoBehaviour
{
	float offsetX;
	float offsetZ;
	private void Start()
	{
		offsetX = Random.Range(0, 1355f);
		offsetZ = Random.Range(0, 1355f);
	}

	float timer = 0;

	private float timerLag = 0;
	bool pickNewTarget = false;
	Vector3 newTargetDir = new Vector3();

	// Update is called once per frame
	void FixedUpdate()
	{
		float staggered = (Mathf.PerlinNoise1D(Time.time + offsetX) * 4f) - 2f;
		// float staggered = 1f;
		var f = 0.400f;
		float smallWibbleWhileHolding = 0;
		float amount = 55f;

		if (Random.Range(0,80) == 0)
		{
			newTargetDir.x = Random.Range(-amount/2f, amount/2f);
			newTargetDir.z = Random.Range(-amount, amount);
		}
		
		// if (staggered>0 && pickNewTarget == true)
		// {
		// 	// timerLag = Mathf.Lerp(timerLag, Time.deltaTime, 2.2f);
		// 	timerLag = Time.deltaTime;
		// 	smallWibbleWhileHolding = 0;
		// 	pickNewTarget = false;
		//
		// 	newTargetDir.x = Random.Range(-amount, amount);
		// 	newTargetDir.z = Random.Range(-amount, amount);
		// }
		// else if(staggered<-0.8f)
		// {
		// 	pickNewTarget = true;
		// 	// timerLag = Mathf.Lerp(timerLag, Time.deltaTime, 0.02f);
		// 	// f /= 10f;
		// 	timerLag = 0;//Time.deltaTime / 14;
		// 	smallWibbleWhileHolding = 0f;
		// }
		// timer += timerLag;
		//
		// float perlinNoise1DX = (-1f + (Mathf.PerlinNoise1D(timer+offsetX) * 2f)) * f + Mathf.PerlinNoise1D(Time.time*3f)*smallWibbleWhileHolding;
		// float perlinNoise1DZ = (-1f + (Mathf.PerlinNoise1D(timer+offsetZ) * 2f)) * f + Mathf.PerlinNoise1D(Time.time*3f)*smallWibbleWhileHolding;
		
		
		
		// transform.localRotation = Quaternion.Euler(perlinNoise1DX, perlinNoise1DZ, 0);
		transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(newTargetDir), f);
	}
}