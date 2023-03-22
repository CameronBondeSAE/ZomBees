using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinD1 : MonoBehaviour
{
    public GameObject brickPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       /* Y = Mathf.PerlinNoise1D(x);
        for (int x = 0; x < 100; x++)
        {
            Vector3 brickPosition = new Vector3();
            brickPosition.x = x;
            brickPosition.y = x;
            Instantiate(brickPrefab, brickPosition, Quaternion.identity);
        }*/
    }
}
