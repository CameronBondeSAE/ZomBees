using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.Effects;
using DG.Tweening;
using Oscar;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Oscar
{
    public class ColourChangeShader : MonoBehaviour
    {
        public float newCutOffHeight;

        public Renderer renderer;

        private BasicBeeEventsManager _basicBeeEventsManager;

        public bool attackPhase;
        
        public float transitionSpeed;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            attackPhase = false;
        }

        private void Update()
        {
            if (attackPhase)
            {
                Attack();
            }
            else
            {
                Search();
            }
        }

        private void Search()
        {
            //dotween stuff     dotween.too
            //DOTween.To(Setter, -1f, 1.5f,transitionSpeed);
            
            //Manuel way to do it:
            //
            newCutOffHeight += Time.deltaTime * transitionSpeed;
            
            if (newCutOffHeight >= 1.5f)
            {
                newCutOffHeight = 1.5f;
            }
            
            renderer.material.SetFloat("_OverTimeValue", newCutOffHeight);
        }
        
        public void Attack()
        {
            //dotween stuff     dotween.too
            //DOTween.To(Setter, 1.5f, -1f,transitionSpeed);

            //Manuel way to do it:
            //
            newCutOffHeight -= Time.deltaTime * transitionSpeed;
            
            if (newCutOffHeight <= -1f)
            {
                newCutOffHeight = -1f;
            }
            
            renderer.material.SetFloat("_OverTimeValue", newCutOffHeight);
        }
        // void Setter(float newValue)
        //  {
        //      renderer.material.SetFloat("_OverTimeValue", newValue);
        //  }

        public void NewColour(Color color)
        {
            renderer.material.SetColor("_OverTimeValue", color);
        }
    }
}