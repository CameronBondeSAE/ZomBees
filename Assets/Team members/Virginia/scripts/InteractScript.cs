using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Virginia
{
    public class InteractScript : MonoBehaviour
    {
      
        void FixedUpdate()
        {
            RaycastHit hitInfo;
             if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 2f))
            {
                Debug.Log("hit");
                ISwitchable thinginfront = hitInfo.transform.GetComponent<ISwitchable>();

            }

        }
    }

}
