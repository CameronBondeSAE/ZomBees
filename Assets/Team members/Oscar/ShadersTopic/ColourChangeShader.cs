using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChangeShader : MonoBehaviour
{
    public Renderer mat;
    public Color color;

    void Update()
    {
        mat.material.SetColor("_Colour", color);
        mat.material.SetFloat("_Blend", Mathf.Sin(Time.deltaTime * 10));
        
    }
}
