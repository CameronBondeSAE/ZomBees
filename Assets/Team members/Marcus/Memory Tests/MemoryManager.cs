using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class MemoryManager : MonoBehaviour
    {
        public FoodAIVision vision;
        public Memory memoryCreator;
        
        public List<Memory> memories;

        private void OnEnable()
        {
            vision.memoryEvent += AddMemories;
        }

        private void AddMemories(GameObject objectSeen)
        {
            var newMemory = memoryCreator.CreateMemory(objectSeen.GetComponent<FakeDynamicObject>());

            if (!memories.Contains(newMemory))
            {
                memories.Add(newMemory);
            }
        }
    }
}
