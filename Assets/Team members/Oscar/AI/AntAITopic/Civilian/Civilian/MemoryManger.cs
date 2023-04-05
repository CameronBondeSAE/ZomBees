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
            memory.description = "Saw a bee";
            memory.timeStamp = Time.time;
            memory.theThing = objSeen.GetComponent<DynamicObject>();
            
        }
    }
}
