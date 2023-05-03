using System;
using System.Collections;
using System.Collections.Generic;
using Johns;
using Lloyd;
using UnityEngine;

namespace Virginia
{
    public class SoundDamager : MonoBehaviour, IHear
    {
        //if object is near sound thingie, it would get hurt
        public float soundHurt;
        public void SoundHeard(SoundProperties soundProperties) 
      {
          if (GetComponent<Health>() != null)
          {
              GetComponent<Health>().Change(soundHurt);
              //SoundHeard would leave it up to the object it is attached to decide how to react

          }
      }
    }
}
