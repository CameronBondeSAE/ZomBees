using System.Collections;
using System.Collections.Generic;
using Anthill.Pool;
using Sirenix.OdinInspector;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

namespace Virginia
{
    public class Inventory : SerializedMonoBehaviour
    {
        public IItem heldItem;
        public float radius;
        public Transform hand;
        
            [Button] 
        public void Pickup()
        {
            Collider[]  CollidersFound = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider colliderFound in  CollidersFound)
            {

                if (colliderFound.GetComponent<IItem>() != null) // checking if its an IItem 
                {
                    heldItem = colliderFound.GetComponent<IItem>();
                    (heldItem as MonoBehaviour).transform.parent = hand;
                    // setting the IItem object to the player's hand, casting it to a monobehaviour
                    (heldItem as MonoBehaviour).GetComponent<Rigidbody>().isKinematic = false; //I forget I need GetComponent sometimes.
                    (heldItem as MonoBehaviour).transform.localPosition = Vector3.zero;
                    //moves it to the position of the "hand"
                    //Debug.Log(message: "pick up");
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
            (heldItem as MonoBehaviour).GetComponent<Rigidbody>().isKinematic = true; 
            (heldItem as MonoBehaviour).transform.parent = null; //unparents the child aka child becomes an orphan 
            heldItem = null; // clears the object from the held Item slot (I forgot the name)
            Debug.Log("disposed item didn't need it");

        }
        
        

    }

  
}
