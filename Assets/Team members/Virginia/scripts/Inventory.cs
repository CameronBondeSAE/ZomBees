using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

namespace Virginia
{
    public class Inventory : MonoBehaviour
    {
        public IItem heldItem;
        [Button] 
        public void Pickup(Vector3 center, float radius)
        {
            Collider[] ItemFound = Physics.OverlapSphere(center, radius);
            foreach (var Itemsfound in ItemFound)
            { 
                GetComponent<IItem>();   
                Debug.Log(message: "pick up");
            }


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
