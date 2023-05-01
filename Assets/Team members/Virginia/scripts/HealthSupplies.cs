using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

namespace Virginia
{
    public class HealthSupplies : DynamicObject, IItem
    {
        public void Consume()
        {
           
        }

        public void Dispose()
        {
         
        }

        public string Description()
        {
            return ("healthy");
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            
        }
    }
}

