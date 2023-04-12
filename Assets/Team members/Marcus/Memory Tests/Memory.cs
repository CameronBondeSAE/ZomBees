using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

namespace Marcus
{
    [Serializable]
    public class Memory
    {
        public Vector2Int location;
        public string description;
        public float timeStamp;
        public DynamicObject thingToRemember;

        public Memory CreateMemory(DynamicObject thing)
        {
            thingToRemember = thing;
            
            Vector3 position = thing.transform.position;
            location = new Vector2Int((int)position.x, (int)position.z);

            description = thing.description;

            timeStamp = Time.time;

            return this;
        }
    }
}
