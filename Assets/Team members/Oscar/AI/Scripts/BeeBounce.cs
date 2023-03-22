using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBounce : MonoBehaviour
{
    [SerializeField] private GameObject childGameObject;
    [SerializeField] private float speed;
    [SerializeField] private float offset;
    
    private void FixedUpdate()
    {
        float y;
        y = Mathf.PerlinNoise(0,Time.time * speed)*2 -1;
        y /= offset;
        childGameObject.transform.localPosition = new Vector3(0, y, 0);
    }
}
