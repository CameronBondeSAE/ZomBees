using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShaderPropertiesFromCode : MonoBehaviour
{
	public Renderer rend;
	public Color    myColour;

// Update is called once per frame
	void Update()
	{
		rend.material.SetColor("_Colour", myColour);
		rend.material.SetFloat("_Blender", Mathf.PerlinNoise1D(Time.time+transform.position.x/10f));
	}

	// Start is called before the first frame update
    void Start()
    {
    }
}
