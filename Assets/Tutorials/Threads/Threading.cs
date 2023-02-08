using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class Threading : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            CSharpJob job = new CSharpJob();
            JobHandle jobHandle = job.Schedule();
        }

        // for (int i = 0; i < 100; i++)
        // {
        //     Thread thread = new Thread(NormalThreads);
        //     thread.Start();
        // }
    }


    // Start is called before the first frame update
    void Update()
    {
        if (InputSystem.GetDevice<Keyboard>().spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Total = " + total);

            total = 0;

            for (int i = 0; i < 100; i++)
            {
                Thread thread = new Thread(NormalThreads);
                thread.Start();
            }

            // We can wait for all 100 threads to finish here
            // Debug.Log("Total = " + total);
        }

        JobHandle jobHandle;
        if (InputSystem.GetDevice<Keyboard>().jKey.isPressed)
        {
            CSharpJob job = new CSharpJob();
            jobHandle = job.Schedule();
        }
    }

    // Shared memory with ALL threads. Be careful of race conditions! Use LOCKING
    public float total = 0;
    public object lockKey = new object();

    // This is running in a thread
    private void NormalThreads()
    {
        // float answer = 0;


        lock (lockKey)
        {
            for (int i = 0; i < 1000; i++)
            {
                total += Mathf.Sqrt(i) + Mathf.PerlinNoise(i * 1.24f, 0);
            }
        }

        // total = total + answer;

        // Debug.Log("I did something! : "+answer);
    }
}