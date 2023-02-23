using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CamGuySensor2))]
public class CamGuySensor2Editor : Editor
{
	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();

		GUIStyle greyedOutStyle = new GUIStyle(GUI.skin.label);
		greyedOutStyle.normal.textColor     = Color.grey;
		greyedOutStyle.onNormal.textColor   = Color.grey;
		greyedOutStyle.hover.textColor      = Color.grey;
		greyedOutStyle.onHover.textColor    = Color.grey;
		greyedOutStyle.active.textColor     = Color.grey;
		greyedOutStyle.onActive.textColor   = Color.grey;
		greyedOutStyle.focused.textColor    = Color.grey;
		greyedOutStyle.onFocused.textColor  = Color.grey;
		greyedOutStyle.normal.background    = null;
		greyedOutStyle.onNormal.background  = null;
		greyedOutStyle.hover.background     = null;
		greyedOutStyle.onHover.background   = null;
		greyedOutStyle.active.background    = null;
		greyedOutStyle.onActive.background  = null;
		greyedOutStyle.focused.background   = null;
		greyedOutStyle.onFocused.background = null;
		greyedOutStyle.alignment            = TextAnchor.MiddleLeft;
		greyedOutStyle.richText             = true;
		greyedOutStyle.wordWrap             = false;
		greyedOutStyle.stretchWidth         = false;
		greyedOutStyle.fixedWidth           = 0;
		greyedOutStyle.stretchHeight        = false;
		greyedOutStyle.fixedHeight          = 0;
		greyedOutStyle.clipping             = TextClipping.Clip;
		greyedOutStyle.imagePosition        = ImagePosition.ImageLeft;
// Disable interaction with the label
		GUI.enabled = false;
		
		GUILayout.TextField("Amount = " + (target as CamGuySensor2)?.amount, greyedOutStyle);
		// Disable interaction with the label
		GUI.enabled = true;
	}
}
