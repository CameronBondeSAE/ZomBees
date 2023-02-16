using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Virginia
{
    public class Switch : MonoBehaviour
    {
        public event Action TurnOnEvent;
        public event Action TurnOffEvent;

        [Button]  // cheat - plugin
        public void TurnOn()
        {
            TurnOnEvent?.Invoke();
        }

        [Button] // cheat - plugin
        public void TurnOff()
        {
            TurnOffEvent?.Invoke();
        }
    }
}
