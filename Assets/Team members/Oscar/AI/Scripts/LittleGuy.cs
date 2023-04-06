using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class LittleGuy : LivingEntity
    {
        public bool isBee;
        public Rigidbody rb;
        public GameObject littleGuyModel;
        public float speed;
        public float turnSpeed;

        public GameObject myHome;

        public List<GameObject> collectedObjects;
        
        private void Awake()
        {
            collectedObjects = new List<GameObject>();
        }
    }
}

