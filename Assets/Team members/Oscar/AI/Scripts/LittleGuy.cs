using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class LittleGuy : DynamicObject
    {
        public bool isBee;
        public Rigidbody rb;
        public GameObject littleGuyModel;
        public float speed;
        public float turnSpeed;

        public List<GameObject> collectedObjects;

        public GameObject myHome;

        private void Awake()
        {
            collectedObjects = new List<GameObject>();
        }
    }
}

