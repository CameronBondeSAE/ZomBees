using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virginia
{
    public class HealthSupplies : MonoBehaviour, IItem
    {
        public void Consume()
        {
           
        }

        public void Dispose()
        {
         
        }

        public string Description()
        {
            throw new System.NotImplementedException(); // it chucks a fit if I remove line 21
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            
        }
    }
}

