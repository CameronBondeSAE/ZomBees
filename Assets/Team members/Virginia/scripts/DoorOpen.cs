using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace V {
    public class DoorOpen : MonoBehaviour
    {
        [Button]  // cheat - plugin
        public void open()
        { gameObject.SetActive(true); }


        [Button]  // cheat - plugin
        public void close()
        { gameObject.SetActive(false); }



    }
}