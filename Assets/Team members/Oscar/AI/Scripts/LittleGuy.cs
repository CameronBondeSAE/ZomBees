using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Lloyd;

namespace Oscar
{
    public class LittleGuy : LivingEntity, IHear
    {
        
        public GameObject littleGuyModel;
        public Rigidbody rb;
        public float speed;
        public float turnSpeed;

        public void SoundHeard(SoundProperties soundProperties)
        {
            throw new NotImplementedException();
        }
    }
}

