using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class MemoryManger : MonoBehaviour
    {
        public List<Memory> memories;

        private Memory memory;
        
        public void AddMemory(GameObject objSeen)
        {
            memory = gameObject.AddComponent<Memory>();
            memory.position = Vector2Int.RoundToInt(objSeen.transform.position);
            memory.description = objSeen.GetComponent<DynamicObject>().description;
            memory.timeStamp = Time.time;
            memory.theThing = objSeen.GetComponent<DynamicObject>();
            
        }

        public Memory FindMemoryOfType<T>()
        {
            foreach (Memory item in memories)
            {
                if (item.theThing.GetType() == typeof(T))
                {
                    return item;
                }
            }

            return null;
        }

        public Memory FindClosestMemoryOfType<T>()
        {
            List<Memory> memoriesToSort = new List<Memory>();
            
            foreach (Memory item in memories)
            {
                if (item.theThing.GetType() == typeof(T))
                {
                    memoriesToSort.Add(item);
                }
                memoriesToSort.Sort((memory1, memory2) => Vector3.Distance(transform.position, memory1.theThing.transform.position) < Vector3.Distance(transform.position, memory2.theThing.transform.position) ? 1 : -1);
            }

            return null;
        }
    }
}
