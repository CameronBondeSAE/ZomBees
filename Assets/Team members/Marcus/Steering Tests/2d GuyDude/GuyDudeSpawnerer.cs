using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class GuyDudeSpawnerer : MonoBehaviour
    {
        public GameObject guyDude;
        public int amount;

        /// <summary>
        /// Number of seconds between each ai is spawned
        /// </summary>
        public float spawnDelay;

        private float spawnTimer;
        public List<GameObject> spawnedAI;

        void Update()
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0 && spawnedAI.Count < amount)
            {
                Spawn();
                spawnTimer = spawnDelay;
            }
        }

        public void Spawn()
        {
            GameObject ai = Instantiate(guyDude, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            spawnedAI.Add(ai);
        }
    }
}
