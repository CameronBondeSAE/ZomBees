using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marcus
{
    public class TurnToFood : TurnFunction
    {
        public Rigidbody rb;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Food>())
            {
                TurnTowards(rb, other.gameObject, 0.005f);
            }
        }
    }
}
