using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BeeWingsManager : MonoBehaviour
{
    public GameObject wing;
    private int numWings;
    public float xDistance;
    public float yDistance;

    public List<GameObject> myWings = new List<GameObject>();
    public List<List<GameObject>> wingPairs = new List<List<GameObject>>();

    [Button]
    void SpawnWings()
    {
        numWings = myWings.Count;
        int currentPair = -1;
        Vector3 startPosition = transform.position - new Vector3((numWings / 2) * xDistance, 0, 0);

        for (int i = 0; i < numWings; i++)
        {
            int pairIndex = i / 2;

            if (i % 2 == 0)
            {
                if (pairIndex >= wingPairs.Count)
                {
                    wingPairs.Add(new List<GameObject>());
                }

                currentPair++;
            }

            GameObject newWing = Instantiate(wing, startPosition + new Vector3((i % 2 == 0 ? 1 : -1) * xDistance, currentPair * yDistance, 0), Quaternion.identity);
            wingPairs[currentPair].Add(newWing);
        }
    }
}