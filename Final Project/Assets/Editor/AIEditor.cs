using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// @author Mike Dobson
/// </summary>

[CustomEditor(typeof(EnemyAIController))]
public class AIEditor : Editor {

    EnemyAIController AIScript;

    void Awake()
    {
        AIScript = (EnemyAIController)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("AIDifficulty"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("activeTrack"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("MAX_SPEED"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("acceleration"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("deceleration"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("MAX_BLACKOUT"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("numLaps"));

    }
}
