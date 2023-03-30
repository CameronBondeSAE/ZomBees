using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virginia
{

    public class LightDisable : MonoBehaviour
    {
        // Start is called before the first frame update
        void OnEnable()
        {
            Debug.Log("light is off");
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("light is still off");
        }

        void OnDisable()
        {
            Debug.Log("light is on");
        }
    }
}