using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class MemoryManager : MonoBehaviour
    {
        public FoodAIVision vision;
        public List<Memory> memories;

        private int counter;

        private void OnEnable()
        {
            vision.memoryEvent += AddMemories;
        }

        private void AddMemories(GameObject objectSeen)
        {
            counter = 0;

            foreach (Memory memory in memories)
            {
                if (memory.thingToRemember.gameObject == objectSeen)
                {
                    counter++;
                }
            }

            if (counter <= 0)
            {
                var newMemory = gameObject.AddComponent<Memory>().CreateMemory(objectSeen.GetComponent<FakeDynamicObject>());
                memories.Add(newMemory);
            }
        }

        private void FixedUpdate()
        {
            foreach (Memory memory in memories)
            {
                if (Time.time - memory.timeStamp >= 75f)
                {
                    memories.Remove(memory);
                    Destroy(memory);
                }
            }
        }
    }
}
