using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateLimiter : MonoBehaviour
{
	public int targetFPS;

	public bool setDuringPlayMode = false;
	
    // Start is called before the first frame update
    void Start()
	{
		Application.targetFrameRate = targetFPS;
	}

    // Update is called once per frame
    void Update()
    {
		if (setDuringPlayMode)
		{
			Application.targetFrameRate = targetFPS;
		}
	}
}
