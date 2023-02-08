using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyDudeSpawner : MonoBehaviour
{
    public GameObject guyDude;
    public int amount;

    private float spawnTimer = 1.5f;
    public List<GameObject> spawnedAI;

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0 && spawnedAI.Count < amount)
        {
            GameObject ai = Instantiate(guyDude, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            spawnedAI.Add(ai);

            spawnTimer = 1.5f;
        }
    }
}
