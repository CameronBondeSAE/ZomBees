using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubePerlinScripting : MonoBehaviour
{
    public float offset;
    public PerlinNoiseSpawner perlinNoiseSpawner;

    private void Start()
    {
        
        offset = Random.Range(-10f, 1000f);
        float x = 0f;
        float y = Mathf.PerlinNoise(offset+Time.time, 0f);
        Vector3 averagedPosition = new Vector3(0f, y*0.5f, 0f);
        transform.localPosition = averagedPosition;
    }

    void Update()
    {
       
    }
}
