using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Team_members.Lloyd.Scripts_L.HearingComponent;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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

