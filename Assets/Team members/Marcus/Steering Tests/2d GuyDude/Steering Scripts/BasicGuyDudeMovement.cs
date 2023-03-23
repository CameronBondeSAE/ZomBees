using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class BasicGuyDudeMovement : MonoBehaviour
    {
        public float speed;
        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.AddRelativeForce(Vector3.forward * speed);
        }
    }
}
