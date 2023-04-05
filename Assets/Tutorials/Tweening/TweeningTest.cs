using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cam
{

    public class TweeningTest : MonoBehaviour
    {
        public Transform target;
        public Vector3   offset;

        public float targetValue;
        public float value;
        public float speed;

        public Color colour;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // value                = Mathf.Lerp(value, targetValue, t);
            // transform.localScale = new Vector3(value, value, value);

            transform.position = Vector3.Lerp(transform.position, target.position + offset, speed);
        }
    }
}