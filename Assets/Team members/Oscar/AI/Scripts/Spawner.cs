using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject TheGuy;
    private Vector3 spawnLoc;

    private void Start()
    {
        spawnLoc = new Vector3(transform.position.x, transform.position.y,transform.position.z);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnGuy();
        }
    }

    private void SpawnGuy()
    {
        Instantiate(TheGuy,spawnLoc,quaternion.identity);
    }
}
