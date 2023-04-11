using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.Effects;
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
            newCutOffHeight += Time.deltaTime * 2;

            if (newCutOffHeight >= 1.5f)
            {
                newCutOffHeight = 1.5f;
            }

            renderer.material.SetFloat("_OverTimeValue", newCutOffHeight);
        }

        public void Attack()
        {
            newCutOffHeight -= Time.deltaTime * 2;

            if (newCutOffHeight <= -1f)
            {
                newCutOffHeight = -1f;
            }

            renderer.material.SetFloat("_OverTimeValue", newCutOffHeight);
        }

        public void NewColour(Color color)
        {
            renderer.material.SetColor("_OverTimeValue", color);
        }
    }
}