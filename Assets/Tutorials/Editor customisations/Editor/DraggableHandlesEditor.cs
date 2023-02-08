using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DraggableHandles))]
public class DraggableHandlesEditor : Editor
{
	protected virtual void OnSceneGUI()
	{
		float size = 0.25f;

		if (Event.current.type == EventType.Repaint)
		{
			Transform transform = ((DraggableHandles) target).transform;
			Handles.color = Handles.xAxisColor;
			Handles.DotHandleCap(0, transform.position + new Vector3(3f, 0f, 0f), transform.rotation * Quaternion.LookRotation(Vector3.right), size, EventType.Repaint);
			Handles.color = Handles.yAxisColor;
			Handles.DotHandleCap(0, transform.position + new Vector3(0f, 3f, 0f), transform.rotation * Quaternion.LookRotation(Vector3.up), size, EventType.Repaint);
			Handles.color = Handles.zAxisColor;
			Handles.DotHandleCap(0, transform.position + new Vector3(0f, 0f, 3f), transform.rotation * Quaternion.LookRotation(Vector3.forward), size, EventType.Repaint);
			
			
		}

		// DrawWireCube
	}
}