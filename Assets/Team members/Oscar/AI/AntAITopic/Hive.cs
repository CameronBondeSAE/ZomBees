using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

public class Hive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<LittleGuy>() != null)
        {
            LittleGuy littleGuy = other.gameObject.GetComponent<LittleGuy>();
            if (littleGuy.collectedObjects.Count >= 1)
            {
                for (int i = 0; i < littleGuy.collectedObjects.Count; i++)
                {
                    //UtilityManager.EnableAfterDelay(littleGuy.collectedObjects[i]);
                }
                littleGuy.collectedObjects.Clear();
            }
        }
    }
}
