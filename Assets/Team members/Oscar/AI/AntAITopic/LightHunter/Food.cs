using System;
using System.ComponentModel;
using Oscar;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

namespace Oscar
{
    public class Food : MonoBehaviour, IItem
    {
        [Sirenix.OdinInspector.ReadOnly]public bool BeeObtained;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<LittleGuy>() != null)
            {
                if (!collision.gameObject.GetComponent<LittleGuy>().collectedObjects.Contains(gameObject))
                {
                    collision.gameObject.GetComponent<LittleGuy>().collectedObjects.Add(gameObject);
                    Pickup(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Hive>() != null)
            {
                BeeObtained = true;
            }
        }

        public string Description()
        {
            //its honey, idk what to place here
            return "Food";
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
