using UnityEngine;
using System.Collections;
using UnityEditor;

/*
 * @author Mike Dobson
 * */

[CustomEditor(typeof(ScriptEngine))]
public class EngineEditor :  Editor
{
	
	ScriptEngine engineScript;

	void Awake()
	{
		engineScript = (ScriptEngine)target;
	}

	public override void OnInspectorGUI()
	{
//		//required things for arrays
		//serializedObject.Update ();
//
//		//-------------------------------
//		//Place your custom editor stuffs
//		//serializedObject.waypoints
		//SerializedProperty movementsArray = serializedObject.FindProperty ("movements");
		//SerializedProperty effectsArray = serializedObject.FindProperty ("effects");
		//SerializedProperty facingsArray = serializedObject.FindProperty ("facings");

        //added by gipson to figure things out
	    // DrawDefaultInspector();
		//EditorGUILayout.PropertyField(serializedObject.FindProperty("infiniteLoopCatcher"));

        if(GUILayout.Button("Editor"))
        {
            EngineWindowEditor window = (EngineWindowEditor)EditorWindow.GetWindow(typeof(EngineWindowEditor));
            window.Show();
        }

		//PrintInformation ();

		#region bye bye
		//EditorGUILayout.Space ();


        //EditorGUILayout.PropertyField(movementsArray);
        //if (movementsArray.isExpanded)
        //{
			//EditorGUILayout.PropertyField(movementsArray.arraySize);
            //EditorGUILayout.PropertyField(movementsArray.FindPropertyRelative("Array.size"));
            //EditorGUI.indentLevel++;
            //for (int i = 0; i < movementsArray.arraySize; i++)
            //{
				//EditorGUILayout.PropertyField(movementsArray.GetArrayElementAtIndex(0));
        //        SerializedProperty showInEditor = movementsArray.GetArrayElementAtIndex(i).FindPropertyRelative("showInEditor");

        //        //showInEditor.boolValue = EditorGUILayout.Foldout(showInEditor.boolValue, "Movement " + ( i + 1 ));

        //        //if (showInEditor.boolValue)
        //        //{
        //            //EditorGUILayout.LabelField("Movement " + (i + 1));
        //            EditorGUILayout.PropertyField(movementsArray.GetArrayElementAtIndex(i));
        //        //}
            //}
            //EditorGUI.indentLevel--;
        //}

        //EditorGUILayout.PropertyField(effectsArray);
		
        //if (effectsArray.isExpanded)
        //{
        //    //EditorGUILayout.PropertyField(waypointsArray.arraySize)
        //    EditorGUILayout.PropertyField(effectsArray.FindPropertyRelative("Array.size"));
        //    EditorGUI.indentLevel++;
        //    for (int i = 0; i < effectsArray.arraySize; i++)
        //    {
        //        SerializedProperty showInEditor = effectsArray.GetArrayElementAtIndex(i).FindPropertyRelative("showInEditor");
				
        //        showInEditor.boolValue = EditorGUILayout.Foldout(showInEditor.boolValue, "Effect " + ( i + 1 ));
				
        //        if (showInEditor.boolValue)
        //        {
        //            //EditorGUILayout.LabelField("Movement " + (i + 1));
        //            EditorGUILayout.PropertyField(effectsArray.GetArrayElementAtIndex(i));
        //        }
        //    }
        //    EditorGUI.indentLevel--;
        //}

        //EditorGUILayout.PropertyField(facingsArray);
		
        //if (facingsArray.isExpanded)
        //{
        //    //EditorGUILayout.PropertyField(waypointsArray.arraySize)
        //    EditorGUILayout.PropertyField(facingsArray.FindPropertyRelative("Array.size"));
        //    EditorGUI.indentLevel++;
        //    for (int i = 0; i < facingsArray.arraySize; i++)
        //    {
        //        SerializedProperty showInEditor = facingsArray.GetArrayElementAtIndex(i).FindPropertyRelative("showInEditor");
				
        //        showInEditor.boolValue = EditorGUILayout.Foldout(showInEditor.boolValue, "Facings " + ( i + 1 ));
				
        //        if (showInEditor.boolValue)
        //        {
        //            //EditorGUILayout.LabelField("Movement " + (i + 1));
        //            EditorGUILayout.PropertyField(facingsArray.GetArrayElementAtIndex(i));
        //        }
        //    }
        //    EditorGUI.indentLevel--;
        //}
        //--------------------------------


        //required things for arrays
        //serializedObject.ApplyModifiedProperties();
		#endregion
	}

	void PrintInformation()
	{
		Debug.Log ("Printing Movement Engine Information!");
		Debug.Log ("Movement Length: " + engineScript.movements.Count);
		
		foreach(ScriptMovements moveScript in engineScript.movements)
		{
			Debug.Log ("\tMovement printing...");
			Debug.Log ("\t" + moveScript.moveType.ToString() + ".");
			Debug.Log ("\tEnd Point Name: " + moveScript.endWaypoint.gameObject.name + ".");
		}
	}
}
