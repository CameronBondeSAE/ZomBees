using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Virginia;
 
namespace Virginia {
public class VPerlin : MonoBehaviour
{
        public float offset;

        private void Start()
        {
            offset = Random.Range(-10f, 1000f);
        }
        void Update()
    {
        float y;
       
        y = Mathf.Sin(offset  + Time.time * 10f);
        y += Mathf.Sin(offset + Time.time * 5f);
        y += Mathf.Sin(offset + Time.time * 2f);
        y += Mathf.Sin(offset + Time.time * 20f);
        y = y / 10f;

            transform.localPosition = new Vector3(0, y, 0);
    }
}
}
