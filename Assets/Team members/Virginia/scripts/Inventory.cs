using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Virginia
{
    public class Inventory : MonoBehaviour
    {
        public IItem heldItem;
        [Button] 
        public void Pickup()
        {
            Debug.Log("pick up");
            
           
        }
        
        [Button] 
        public void Consume()
        {
            Debug.Log("conumsed yummy");
        } 
        [Button] 
        public void Dispose()
        {
            Debug.Log("disposed item didn't need it");
        }
        
        

    }
}
