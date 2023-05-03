using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundStart : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(0, 5f));
        GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1f);
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
