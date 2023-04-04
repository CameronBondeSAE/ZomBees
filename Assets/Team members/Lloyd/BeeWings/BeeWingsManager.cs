using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

public class BeeWingsManager : MonoBehaviour
{
    //ChatGPT cowritten
    
    //BeeWings Manager spawns a custom number of wings at a custom xydistance apart
    //
    //pairs are tracked, so that each second wing is flipped horizontally when Instantiated
    //
    //perlin offset is added to each pair
    
    public GameObject wing;
    private GameObject wingParent;
    private int numWings;
    public float xDistance;
    public float yDistance;
    public float zDistance;

    public List<GameObject> myWings = new List<GameObject>();
    public List<List<GameObject>> wingPairs = new List<List<GameObject>>();

    [Button ("CHANGE ANGLE / SPEED / ISALIVE")]
    public void ChangeBeeWingStats(float newAngle, float newSpeed, bool isAlive)
    {
        OnChangeStatEvent(newAngle, newSpeed, isAlive);
    }

    [Button]
    public void AddWing()
    {
        myWings.Add(wing);
    }

    [Button]
    void SpawnWings()
    {
        wingParent = new GameObject("BeeWings Anchor");
        
        numWings = myWings.Count;
        int currentPair = -1;
        Vector3 startPosition = transform.position - new Vector3((numWings / 2) * xDistance, 0, (numWings / 2) * zDistance);
        
        float offset = RandomOffset();

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
            if (i % 3 == 0)
            {
                offset = RandomOffset();
            }

            GameObject newWing = Instantiate(wing, startPosition + new Vector3((i % 2 == 0 ? 1 : -1) * xDistance, currentPair * yDistance, currentPair*zDistance), Quaternion.Euler(0, i % 2 == 0 ? 0 : 180, 0));
            wingPairs[currentPair].Add(newWing);

            BeeWing wingScript = newWing.GetComponent<BeeWing>();
            wingScript.randomOffset = offset;
            ChangeStatEvent += wingScript.ChangeWingStats;
            wingScript.StartFlapping();

            newWing.transform.SetParent(wingParent.transform);
        }
    }
    
    public float RandomOffset()
    {
        float newRand = UnityEngine.Random.Range(-1f, 1f);
        return newRand;
    }

    [Button]
    public void DeleteWings()
    {
        foreach (GameObject deletedWing in myWings)
        {
            BeeWing wingScript = deletedWing.GetComponent<BeeWing>();
            ChangeStatEvent -= wingScript.ChangeWingStats;
        }
        myWings.Clear();
        wingPairs.Clear();
        if (wingParent != null)
        {
            Destroy(wingParent);
        }
    }

    public event Action<float, float, bool> ChangeStatEvent;
    public void OnChangeStatEvent(float newAngle, float newSpeed, bool isAlive)
    {
        ChangeStatEvent?.Invoke(newAngle, newSpeed, isAlive);
    }
}