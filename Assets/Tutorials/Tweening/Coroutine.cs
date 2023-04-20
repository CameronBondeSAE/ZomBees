using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    public IEnumerator coroutine;
    
    [Button]
    void Go()
    {
        coroutine = AFunction();
        
        StartCoroutine(coroutine);
    }

    [Button]
    void Stop()
    {
        StopCoroutine(coroutine);
    }

// Update is called once per frame
    IEnumerator AFunction()
    {
        while (true)
        {
            Debug.Log("Hello");
            // Wait for a bit
            yield return new WaitForSeconds(1f);
            Debug.Log("Goodbye");
            yield return new WaitForSeconds(1f);
        }
    }
}
