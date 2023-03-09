using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Virginia {
    public class DoorOpen : MonoBehaviour, ISwitchable
    {
        [Button]  // cheat - plugin
        public void TurnOn()
        { 
            
            gameObject.SetActive(true);
            

        }


        [Button]  // cheat - plugin
        public void TurnOff()
        {
            
            gameObject.SetActive(false);
        
        
        }



    }
}