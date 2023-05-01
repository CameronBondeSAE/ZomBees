using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class Nodes
    {
        public Vector3 worldPos;

        public bool isBlocked;

        public Vector2Int gridPos;

        public Vector3 startPos;
        
        public bool isFilled;
        
        public Nodes parent;
    }
}
