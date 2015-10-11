using UnityEngine;
using System.Collections;
using UnityEditor;
/// <summary>
/// @author Mike Dobson
/// </summary>

[CustomEditor(typeof(ScriptEngine))]
public class EngineEditor : Editor {

    ScriptEngine engineScript;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //------------------------

        SerializedProperty movementsList = serializedObject.FindProperty("movements");

        EditorGUILayout.PropertyField(movementsList);

        SerializedProperty speed = serializedObject.FindProperty("speed");
        EditorGUILayout.PropertyField(speed);

        if(movementsList.isExpanded)
        {
            EditorGUILayout.PropertyField(movementsList.FindPropertyRelative("Array.size"));
            EditorGUI.indentLevel++;
            for(int i = 0; i < movementsList.arraySize; i++)
            {
                SerializedProperty showInEditor = movementsList.GetArrayElementAtIndex(i).FindPropertyRelative("showInEditor");
                showInEditor.boolValue = EditorGUILayout.Foldout(showInEditor.boolValue, "Movement " + (i + 1));

                if(showInEditor.boolValue)
                {
                    EditorGUILayout.PropertyField(movementsList.GetArrayElementAtIndex(i));
                }
            }
        }

        //========================
        serializedObject.ApplyModifiedProperties();
    }
}
