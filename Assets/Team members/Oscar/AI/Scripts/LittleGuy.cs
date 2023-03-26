using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

namespace Oscar
{
    public class LittleGuy : AntAIState
    {
        public Rigidbody rb;
        public GameObject littleGuyModel;
        public float speed;
        public float turnSpeed;

        public List<GameObject> collectedObjects;

        public GameObject myHive;

        private void Awake()
        {
            collectedObjects = new List<GameObject>();
        }
    }
}

