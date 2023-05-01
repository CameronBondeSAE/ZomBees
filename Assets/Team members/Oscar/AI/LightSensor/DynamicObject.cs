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
        public bool isCiv;
        public bool IsBee
        {
            get { return IsBee; }
            set { isBee = value; }
        }

        public float importance = 1f;
        public string description;
    }
}