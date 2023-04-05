using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweeningTest : MonoBehaviour
{
    public float value;
    public float t;
    public GameObject targetValue;
    public float speed; 


    private void Update()
    {
        // value = Mathf.MoveTowards(value, targetValue, t);
        // transform.localScale = new Vector3(value, value, value);

        transform.position = Vector3.Lerp(transform.position, targetValue.transform.position, speed);
    }
}
