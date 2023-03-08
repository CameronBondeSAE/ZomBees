using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTrigger : MonoBehaviour
{
    private bool blocked;

    private void OnEnable()
    {
    }

    public void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            Debug.Log("blocked");
            blocked = true;
        }
        else
        {
            Debug.Log("unblocked");
            blocked = false;
        }
    }
}