using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class DynamicObject : MonoBehaviour
    {
        //perception levels
        public bool isLit;
        public bool isMoving;
        public bool isFood;
        public bool isObject;

        public float importance;
        public string description;
        
    }
}