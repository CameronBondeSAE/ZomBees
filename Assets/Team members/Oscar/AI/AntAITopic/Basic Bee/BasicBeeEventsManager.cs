using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oscar
{
    public class BasicBeeEventsManager : MonoBehaviour
    {
        public event Action attackThing;

        public event Action searchThing;

        public void attackThingEvent()
        {
            attackThing?.Invoke();
        }

        public void SearchThingEvent()
        {
            searchThing?.Invoke();
        }
    }
}
