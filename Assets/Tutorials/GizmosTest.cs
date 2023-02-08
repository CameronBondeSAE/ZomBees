using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GizmosTest : MonoBehaviour
{
	public Transform target;
	public Vector3   fakeTarget;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		target.position = fakeTarget;
		
		Debug.DrawLine(transform.position, Vector3.zero, Color.red);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube(target.position, Vector3.one);
		Gizmos.DrawIcon(transform.position, "Icon_PrimitiveShapes_Stair", true, Color.magenta);
		// Gizmos.DrawGUITexture();
	}
	
}
