using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

namespace Oscar
{
    public class Honey : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<LittleGuy>())
            {
                UtilityManager.DeleteAfterDelay(gameObject);
            }
        }
    }
}
