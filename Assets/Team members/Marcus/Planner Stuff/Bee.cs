using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Marcus
{
    public class Bee : LivingEntity
    {
        [Range(0,1)]
        public float beeness;

        private Renderer renderer;
        
        private void Start()
        {
            renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            renderer.material.SetFloat("_Beeness", beeness);
        }
    }
}
