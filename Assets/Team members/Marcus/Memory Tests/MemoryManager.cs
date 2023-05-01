using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oscar;

namespace Marcus
{
    public class MemoryManager : MonoBehaviour
    {
        public OscarVision finalVision;
        
        public List<Memory> memories;

        public float baseLifetime = 75f;

        private bool alreadyExists;

        
        List<Memory> toRemove = new List<Memory> (20);

        private void OnEnable()
        {
            if (finalVision != null) finalVision.objectSeenEvent += AddMemory;

            StartCoroutine(RemoveMemories());
        }

        public void AddMemory(GameObject objectSeen)
        {
            alreadyExists = false;

            foreach (Memory memory in memories)
            {
                if (memory.thingToRemember != null && memory.thingToRemember.gameObject == objectSeen )
                {
                    // Update time and position and another other info (description?)
                    UpdateMemory(memory);
                    alreadyExists = true;
                }
            }

            if (!alreadyExists)
            {
                Memory newMemory = new Memory().CreateMemory(objectSeen.GetComponent<DynamicObject>());
                memories.Add(newMemory);
            }
            
            // Sort by importance in each memory
            memories.Sort(Comparison);
        }

        int Comparison(Memory x, Memory y)
        {
            if (Math.Abs(x.thingToRemember.importance - y.thingToRemember.importance) < 0.01f)
            {
                return 0;
            }

            if (x.thingToRemember.importance > y.thingToRemember.importance)
                return -1;
            else
                return 1;
        }

        private IEnumerator RemoveMemories()
        {
            while (true)
            {
                foreach (Memory memory in memories)
                {
                    if (Time.time - memory.timeStamp >= baseLifetime * memory.thingToRemember.importance)
                    {
                        toRemove.Add(memory);
                    }
                }

                // Clear out old memories
                if (toRemove.Count>0)
                {
                    foreach (Memory memory in toRemove)
                    {
                        // I'm removing from the main memories list
                        memories.Remove(memory);
                    }
            
                    toRemove.Clear();
                }

                yield return new WaitForSeconds(3.5f);
            }
        }
        
        private void UpdateMemory(Memory memoryToUpdate)
        {
            memoryToUpdate.timeStamp = Time.time;

            Vector3 pos = memoryToUpdate.thingToRemember.gameObject.transform.position;
            memoryToUpdate.location = new Vector2Int((int)pos.x, (int)pos.z);
        }
    }
}
