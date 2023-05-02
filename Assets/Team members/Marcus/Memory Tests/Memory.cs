using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using Oscar;
using UnityEngine;
using UnityEngine.Serialization;

namespace Marcus
{
    [Serializable]
    public class Memory
    {
        public string gridLocation;
        public string description;
        public float timeStamp;
        public DynamicObject thingToRemember;

        // public Memory CreateMemory(DynamicObject thing)
        // {
        //     thingToRemember = thing;
        //     
        //     Vector3 position = thing.transform.position;
        //     gridLocation = ZombeeGameManager.Instance.ConvertWorldSpaceToGridSpace(new Vector2Int((int)position.x, (int)position.z));
        //
        //     description = thing.description;
        //
        //     timeStamp = Time.time;
        //
        //     return this;
        // }
    }
}
