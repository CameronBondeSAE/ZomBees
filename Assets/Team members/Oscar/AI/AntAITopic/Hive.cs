using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class Hive : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<LittleGuy>() != null)
        {
            LittleGuy littleGuy = collision.gameObject.GetComponent<LittleGuy>();
            if (littleGuy.collectedObjects.Count >= 1)
            {
                for (int i = 0; i < littleGuy.collectedObjects.Count; i++)
                {
                    UtilityManager.EnableAfterDelay(littleGuy.collectedObjects[i]);
                }
            }
        }
    }
}
