using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Marcus
{
    public class GuyDudeWander : MonoBehaviour
    {
        private Rigidbody rb;

        private float xOffset;
        private float zOffset;
        public float force;

        // Start is called before the first frame update
        void Start()
        {
            xOffset = Random.Range(-1000, 1000);
            zOffset = Random.Range(-1000, 1000);

            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float x = xOffset + Time.time;
            float z = zOffset + Time.time;

            float wander = Mathf.PerlinNoise(x, z) * 2 - 1;
            rb.AddRelativeTorque(0, wander * force, 0);
        }
    }
}