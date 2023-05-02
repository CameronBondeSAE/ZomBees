using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavmeshTest : MonoBehaviour
{
	public NavMeshAgent navMeshAgent;
	public float        arrivedDistance = 1.5f;

	// Debugging
	public Transform target;
	private NavMeshPath path;
	private float elapsed = 0.0f;
	
	// Start is called before the first frame update
	void Start()
	{
		// Debugging
		path = new NavMeshPath();
		elapsed = 0.0f;
		
		
		// FindRandomSpot();
	}

	
	[Button]
	public Vector3 FindRandomSpot()
	{
		int     index       = Random.Range(0, PatrolManager.singleton.pathsWithIndoors.Count);
		Vector3 finalTarget = Vector3.zero;
		bool    foundTarget = false;
		
		// Find a non-null entry
		int bailOutCount = 0;
		while (PatrolManager.singleton.pathsWithIndoors[index] == null)
		{
			index = Random.Range(0, PatrolManager.singleton.pathsWithIndoors.Count);
			bailOutCount++;
			if (bailOutCount > 100)
				break;
		}
		finalTarget = PatrolManager.singleton.pathsWithIndoors[index].transform.position;

		if (PatrolManager.singleton.pathsWithIndoors[index] != null)
		{
			navMeshAgent.SetDestination(finalTarget);
			return finalTarget;
		}

		return Vector3.zero; // HACK won't really know if it succeeded. Should be bool or something
	}
	
	
	// CHECK: Does this do everything here? NavMeshPathStatus.PathComplete
	
	// public bool ReachedDestinationOrGaveUp()
	// {
	//
	// 	if (!navMeshAgent.pathPending)
	// 	{
	// 		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + arrivedDistance)
	// 		{
	// 			if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
	// 			{
	// 				return true;
	// 			}
	// 		}
	// 	}
	//
	// 	return false;
	// }

	// Update is called once per frame
	void Update()
	{
		if (!navMeshAgent.pathPending && !navMeshAgent.hasPath) 
		{
			Debug.Log ("I have reached my destination!");
			FindRandomSpot();
		}
		// if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
		// {
		// 	Debug.Log(gameObject.name + " got to the destination");
		// 	FindRandomSpot();
		// }	
		
		
		// Debugging
		// Update the way to the goal every second.
		// elapsed += Time.deltaTime;
		// if (elapsed > 1.0f)
		// {
		// 	elapsed -= 1.0f;
		// 	NavMesh.CalculatePath(transform.position, randomSpot, NavMesh.AllAreas, path);
		// }
		// for (int i = 0; i < path.corners.Length - 1; i++)
		// 	Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 1f);
	}
	
}