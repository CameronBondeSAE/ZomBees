using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Oscar
{
    public class Wonder : Oscar.StateBase
    {
        public LittleGuy guy;

        private float perlin;
        private float scale = 10f;
        private float zoomX = 1.15f;
        private float zoomZ = 1.15f;
        
        
        private float randomness;

        private void Start()
        {
            zoomX = Random.Range(-0.5f, 0.5f);
            zoomZ = Random.Range(-0.5f, 0.5f);
        }

        void FixedUpdate()
        {
            float x = zoomX + Time.time;// * scale;
            float z = zoomZ + Time.time;// * scale;
            
            perlin = Mathf.PerlinNoise(x,z)*2-1;
            
            guy.rb.AddRelativeTorque(0,perlin,0);
        }
        
        #region stateRegions

        public override void Enter()
        {
            Debug.Log("AVOID IT!");
            base.Enter();
        }
        
        public override void Execute()
        {
            Debug.Log("Execute");
            base.Execute();
        }
        
        public override void Exit()
        {
            Debug.Log("Exit");
            base.Exit();
        }

        #endregion
    }
}

