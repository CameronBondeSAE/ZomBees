using System.Collections;
using System.Collections.Generic;
using Anthill.Pool;
using Oscar;
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
            foreach (Collider colliderFound in CollidersFound)
            {
                
                if (colliderFound.GetComponent<IItem>() != null) // checking if its an IItem 
                {
                    heldItem = colliderFound.GetComponent<IItem>();
                    DynamicObject dynamicObject = (heldItem as DynamicObject);
                    dynamicObject.transform.parent = hand;
                    // setting the IItem object to the player's hand, casting it to a monobehaviour
                    dynamicObject.GetComponent<Rigidbody>().isKinematic = true; //I forget I need GetComponent sometimes.
                    dynamicObject.transform.localPosition = Vector3.zero; //moves it to the position of the "hand"
                    dynamicObject.transform.rotation = Quaternion.identity; // allows for the object not fall in weird position 

                    //Debug.Log(message: "pick up");
                    heldItem.Pickup(gameObject);

                    foreach (Collider inChild in dynamicObject.GetComponentsInChildren<Collider>())
                    {
                        inChild.enabled = false;
                    }

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
            if (heldItem != null)
            {
                heldItem.Dispose();
                DynamicObject dynamicObject   = (heldItem as DynamicObject);
                Vector3       playerDirection = dynamicObject.transform.forward;                                      //Oscar added
                Vector3       objectPosition  = dynamicObject.transform.position + (playerDirection * placeDistance); //Oscar added
                dynamicObject.transform.position = objectPosition;                                                    //Oscar added

                dynamicObject.GetComponent<Rigidbody>().isKinematic = false;
                dynamicObject.transform.parent                      = null; //unparents the child aka child becomes an orphan 


                foreach (Collider inChild in dynamicObject.GetComponentsInChildren<Collider>())
                {
                    inChild.enabled = true;
                }
            }

            heldItem = null; // clears the object from the held Item slot (I forgot the name)
            //Debug.Log("disposed item didn't need it");
        }
    }
}
