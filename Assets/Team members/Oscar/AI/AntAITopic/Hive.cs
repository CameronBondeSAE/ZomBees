using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;
using Virginia;

public class Hive : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Inventory>() != null)
        {
            Inventory inventory = other.gameObject.GetComponent<Inventory>();
            if (inventory.heldItem != null)
            {
                //inventory.heldItem.Dispose();
                inventory.Dispose();
            }
        }
    }
}
