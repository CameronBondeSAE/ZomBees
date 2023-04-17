using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

namespace Virginia
{
    public class Inventory : SerializedMonoBehaviour
    {
        public IItem heldItem;
        public float radius;
        public object hand;
        [Button] 
        public void Pickup()
        {
            Collider[]  ItemsFound = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider ItemFound in  ItemsFound)
            {
                if (ItemFound.GetComponent<Collider>() != null )
                {
                   // heldItem = ItemFound.GetComponent<IItem>();
                    Debug.Log(message: "pick up");
                    


                }
                
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
         //transform.parent = null;

        }
        
        

    }

  
}
