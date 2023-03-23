using System;
using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;

namespace Oscar
{
    public class Honey : MonoBehaviour, IItem
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<LittleGuy>())
            {
                collision.gameObject.GetComponent<LittleGuy>().collectedObjects.Add(gameObject);
                Pickup(gameObject);
            }
        }
        
        public string Description()
        {
            //its honey, idk what to place here
            return "Honey";
        }

        public void Pickup(GameObject obj)
        {
            //just disable the object until needed later
            UtilityManager.DisableAfterDelay(obj);
        }

        public void Consume()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
