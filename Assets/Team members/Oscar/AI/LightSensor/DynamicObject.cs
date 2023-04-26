using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class DynamicObject : SerializedMonoBehaviour
    {
        //perception levels
        public bool isLit;
        public bool isMoving;
        public bool isFood;
        public bool isObject;
        public bool isBee;

        public float importance;
        public string description;
    }
}