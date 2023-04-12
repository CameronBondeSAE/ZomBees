using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lloyd
{


    public class ShaderScript : MonoBehaviour
    {
        private Renderer rend;

        private void Awake()
        {
            rend = GetComponent<Renderer>();
        }

        [Button]
        public void ChangeColor(Color newColor)
        {
            rend.material.SetColor("_RedColor", newColor);
        }
    }
}