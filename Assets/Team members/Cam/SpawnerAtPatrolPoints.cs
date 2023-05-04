using Oscar;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAtPatrolPoints : MonoBehaviour
{
	public int        number = 5;
	public GameObject thingToSpawn;

	[SerializeField]
	bool indoor = true;

	[Button]
	// Start is called before the first frame update
	public void Spawn()
	{
		PatrolPoint point;
		for (int i = 0; i < number; i++)
		{
			if (indoor)
			{
				point = PatrolManager.singleton.indoors[Random.Range(0, PatrolManager.singleton.indoors.Count)];
			}
			else
			{
				point = PatrolManager.singleton.paths[Random.Range(0, PatrolManager.singleton.indoors.Count)];
			}
			Instantiate(thingToSpawn, point.transform.position, Quaternion.Euler(0, Random.Range(-180, 180), 0));
		}
	}
}