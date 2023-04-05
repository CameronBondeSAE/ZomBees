using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AFunction());
        Debug.Log("CAM!");
    }

    // Update is called once per frame
    IEnumerator AFunction()
    {
        while (true)
        {
            Debug.Log("Hello");
            // Wait for a bit
            yield return new WaitForSeconds(2f);
            Debug.Log("Goodbye");
            yield return new WaitForSeconds(2f);
            Debug.Log("Goodbye");
            yield return new WaitForSeconds(2f);
            Debug.Log("Goodbye");
        }
    }
}
