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
    public class LittleGuy : CharacterBase, IHear, IInteractable
    {
        public Rigidbody rb;
        public float speed;
        public float turnSpeed;
        public Health health;
        
        private void Update()
        {
            if (health.currHealth <= 0)
            {
                UtilityManager.DeleteAfterDelay(gameObject);
            }
        }

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

