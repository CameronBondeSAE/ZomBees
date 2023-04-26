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
    public class LittleGuy : LivingEntity, IHear, IInteractable
    {
        public Rigidbody rb;
        public float speed;
        public float turnSpeed;

        public void SoundHeard(SoundProperties soundProperties)
        {
            
        }

        public void Interact()
        {
            
        }

        public void Inspect()
        {
            throw new NotImplementedException();
        }
    }
}

