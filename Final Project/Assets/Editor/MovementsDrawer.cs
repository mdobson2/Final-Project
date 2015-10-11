using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// @author Mike Dobson
/// </summary>

[CustomPropertyDrawer(typeof(ScriptMovements))]
public class MovementsDrawer : PropertyDrawer {

    ScriptMovements movementScript;
    float extraHeight = 60f;
    float displaySize = 20f;
    float numDisplays = 0f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        SerializedProperty movementType = property.FindPropertyRelative("moveType");
        numDisplays = 0;

        Rect movementDisplay = GetNewPosition(position);
        EditorGUI.PropertyField(movementDisplay, movementType);
        numDisplays++;

        SerializedProperty endWaypoint = property.FindPropertyRelative("endWaypoint");
        movementDisplay = GetNewPosition(position);
        EditorGUI.PropertyField(movementDisplay, endWaypoint);
        numDisplays++;

        if(movementType.enumValueIndex == (int)MovementTypes.BEZIER)
        {
            SerializedProperty curveWaypoint = property.FindPropertyRelative("curveWaypoint");
            movementDisplay = GetNewPosition(position);
            EditorGUI.PropertyField(movementDisplay, curveWaypoint);
        }

        EditorGUI.EndProperty();
    }

    public Rect GetNewPosition(Rect position)
    {
        return new Rect(position.x, position.y + displaySize * numDisplays, position.width, 15f);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + (extraHeight);
    }
}
