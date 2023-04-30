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
        public IItem heldItem; //Item that is/ Isn't in the player's hand
        public float radius;
        public Transform hand; //where we want the heldItem to be

        public float placeDistance; //how far forward to place it
        
            [Button] 
        public void Pickup()
        {
            
            
            Collider[]  CollidersFound = Physics.OverlapSphere(hand.transform.position, radius);
            foreach (Collider colliderFound in  CollidersFound)
            {
                
                if (colliderFound.GetComponent<IItem>() != null) // checking if its an IItem 
                {
                    heldItem = colliderFound.GetComponent<IItem>();
                    (heldItem as MonoBehaviour).transform.parent = hand;
                    // setting the IItem object to the player's hand, casting it to a monobehaviour
                    (heldItem as MonoBehaviour).GetComponent<Rigidbody>().isKinematic = true; //I forget I need GetComponent sometimes.
                    (heldItem as MonoBehaviour).transform.localPosition = Vector3.zero; //moves it to the position of the "hand"
                    (heldItem as MonoBehaviour).transform.rotation = Quaternion.identity; // allows for the object not fall in weird position 

                    //Debug.Log(message: "pick up");
                    heldItem.Pickup(gameObject);
                    break;
                }
            }
            
        }
        [Button] 
        public void Consume()
        {
            heldItem.Consume();
            
        } 
        [Button] 
        public void Dispose()
        {
            heldItem.Dispose();
            Vector3 playerDirection = (heldItem as MonoBehaviour).transform.forward; //Oscar added
            Vector3 objectPosition = (heldItem as MonoBehaviour).transform.position + (playerDirection * placeDistance); //Oscar added
            (heldItem as MonoBehaviour).transform.position = objectPosition; //Oscar added
            
            (heldItem as MonoBehaviour).GetComponent<Rigidbody>().isKinematic = false; 
            (heldItem as MonoBehaviour).transform.parent = null; //unparents the child aka child becomes an orphan 

            heldItem = null; // clears the object from the held Item slot (I forgot the name)
            //Debug.Log("disposed item didn't need it");
        }
    }
}
