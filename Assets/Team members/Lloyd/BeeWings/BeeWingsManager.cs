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

        Vector3 startPosition = transform.position - new Vector3((numWings / 2) * xDistance, 0, 0);

        for (int i = 0; i < numWings; i++)
        {
            GameObject newWing = Instantiate(wing, startPosition + new Vector3(i * xDistance, 0, 0), Quaternion.identity);
            int pairIndex = i / 2;

            if (i % 2 == 0)
            {
                // Create a new pair list if one doesn't exist for this pair index
                if (pairIndex >= wingPairs.Count)
                {
                    wingPairs.Add(new List<GameObject>());
                }

                wingPairs[pairIndex].Add(newWing);
            }
            else
            {
                // Add to the existing pair list
                wingPairs[pairIndex].Add(newWing);

                // Add yDistance to the y-coordinate of the pair's position
                foreach (GameObject wing in wingPairs[pairIndex])
                {
                    Vector3 pos = wing.transform.position;
                    pos.y += yDistance;
                    wing.transform.position = pos;
                }
            }
        }
    }
}
