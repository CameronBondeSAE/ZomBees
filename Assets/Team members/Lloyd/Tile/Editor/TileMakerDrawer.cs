using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

//another Chat GPT collaboration
[CustomPropertyDrawer(typeof(TileLevelMaker))]
public class TileLevelMakerDrawer : PropertyDrawer
{
    private const int boardSize = 16;
    private const float fieldSize = 20f;
    [ExecuteInEditMode]
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        float startX = position.x;
        float startY = position.y + EditorGUIUtility.singleLineHeight;
        float squareSize = 1;

        // Draw the squares
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                Rect squarePosition = new Rect(startX + x * squareSize, startY + y * squareSize, squareSize, squareSize);
                SerializedProperty squareProperty = property.FindPropertyRelative("board").GetArrayElementAtIndex(y * boardSize + x);

                EditorGUI.PropertyField(squarePosition, squareProperty, GUIContent.none);
            }
        }
        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return fieldSize + EditorGUIUtility.singleLineHeight;
    }
    
    
}