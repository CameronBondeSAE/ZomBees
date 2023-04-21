using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lloyd
{
    public class ShaderScript : MonoBehaviour
    {
        public Renderer rend;

        public Material myMaterial;

        private void Awake()
        {
            rend = GetComponent<Renderer>();
            myMaterial = rend.materials[2];
        }

        public void ChangeColorRed()
        {
            Color newColor = Color.red;
            myMaterial.SetColor("_RedColor", newColor);
        }
        
        public void ChangeColorGreen()
        {
            Color newColor = new Color(0, .4f, .1f, 1);
            myMaterial.SetColor("_RedColor", newColor);
        }
        
        public void ChangeColorOrange()
        {
            Color newColor = new Color(1, .6f, .2f, 1);
            myMaterial.SetColor("RedColor", newColor);
        }
        
        public void ChangeColorPurple()
        {
            Color newColor = new Color(.4f, 0, .8f, 1);
            myMaterial.SetColor("_RedColor", newColor);
        }
    }
}