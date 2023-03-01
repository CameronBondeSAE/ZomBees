using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class RandomNavmeshTest : MonoBehaviour
{
	public NavMeshAgent navMeshAgent;

	// Start is called before the first frame update
	void Start()
	{
		FindRandomSpot();
	}

	
	[Button]
	public void FindRandomSpot()
	{
		int     index       = Random.Range(0, PatrolManager.singleton.pathsWithIndoors.Count);
		Vector3 finalTarget = Vector3.zero;
		bool    foundTarget = false;
		
		// Find a non-null entry
		while (PatrolManager.singleton.pathsWithIndoors[index] == null)
		{
			index = Random.Range(0, PatrolManager.singleton.pathsWithIndoors.Count);
		}
		finalTarget = PatrolManager.singleton.pathsWithIndoors[index].transform.position;

		if (PatrolManager.singleton.pathsWithIndoors[index] != null)
		{
			navMeshAgent.SetDestination(finalTarget);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (ReachedDestinationOrGaveUp())
		{
			FindRandomSpot();
		}	
	}
	
	public bool ReachedDestinationOrGaveUp()
	{

		if (!navMeshAgent.pathPending)
		{
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
		}

		return false;
	}
}