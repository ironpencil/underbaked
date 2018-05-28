using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConnectionListAttribute))]
public class ConnectionListPropertyDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //Draw label
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        ////Don't make child fields be indented
        //var indent = EditorGUI.indentLevel;
        //EditorGUI.indentLevel = 0;

        float width = position.width / 3.0f;
        //Calculate rects
        Rect fromRect = new Rect(position.x, position.y, width, position.height);
        Rect doorRect = new Rect(fromRect.xMax, position.y, width, position.height);
        Rect toRect = new Rect(doorRect.xMax, position.y, width, position.height);

        EditorGUI.PropertyField(fromRect, property.FindPropertyRelative("from"), GUIContent.none);
        EditorGUI.PropertyField(doorRect, property.FindPropertyRelative("door"), GUIContent.none);
        EditorGUI.PropertyField(toRect, property.FindPropertyRelative("to"), GUIContent.none);

        ////Calculate rects
        //Rect fromRect = new Rect(position.x, position.y, 120, position.height);
        //Rect betweenRect = new Rect(fromRect.xMax + 2, position.y, 30, position.height);
        //Rect toRect = new Rect(betweenRect.xMax + 2, position.y, 120, position.height);

        //EditorGUI.PropertyField(fromRect, property.FindPropertyRelative("from"), GUIContent.none);
        //EditorGUI.LabelField(betweenRect, "<->");
        //EditorGUI.PropertyField(toRect, property.FindPropertyRelative("to"), GUIContent.none);

        ////Set indent back to what it was
        //EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
