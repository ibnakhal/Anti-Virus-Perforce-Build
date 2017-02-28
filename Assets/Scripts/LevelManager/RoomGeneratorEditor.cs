using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor : Editor {
	bool showPositionRoom, showPositionAmm = true;
	string RoomText = "Rooms";
	string AmountText = "Amount";
	public override void OnInspectorGUI()
	{
		//RoomGenerator myTarget = (RoomGenerator)target;
		serializedObject.Update ();
		serializedObject.ApplyModifiedProperties ();

		showPositionRoom = EditorGUILayout.Foldout (showPositionRoom, RoomText);
		EditorGUI.BeginChangeCheck ();
		EditorGUI.indentLevel++;
		if (showPositionRoom) {
			
			SerializedProperty bossRoom = serializedObject.FindProperty ("bossRoom");
			EditorGUILayout.PropertyField (bossRoom, true);	

			SerializedProperty enemiesRoom = serializedObject.FindProperty ("enemiesRoom");
			EditorGUILayout.PropertyField (enemiesRoom, true);	

			SerializedProperty lootRoom = serializedObject.FindProperty ("lootRoom");
			EditorGUILayout.PropertyField (lootRoom, true);	

			SerializedProperty miscRoom = serializedObject.FindProperty ("miscRoom");
			EditorGUILayout.PropertyField (miscRoom, true);	

			SerializedProperty starterRoom = serializedObject.FindProperty ("starterRoom");
			EditorGUILayout.PropertyField (starterRoom, true);	
		}

		EditorGUI.indentLevel--;

		showPositionAmm = EditorGUILayout.Foldout (showPositionAmm, AmountText);
		EditorGUI.indentLevel++;
		if (showPositionAmm) {
			SerializedProperty bossRoomAmm = serializedObject.FindProperty ("bossRoomAmm");
			EditorGUILayout.PropertyField (bossRoomAmm, true);	

			SerializedProperty enemiesRoomAmm = serializedObject.FindProperty ("enemiesRoomAmm");
			EditorGUILayout.PropertyField (enemiesRoomAmm, true);	

			SerializedProperty lootRoomAmm = serializedObject.FindProperty ("lootRoomAmm");
			EditorGUILayout.PropertyField (lootRoomAmm, true);	

			SerializedProperty miscRoomAmm = serializedObject.FindProperty ("miscRoomAmm");
			EditorGUILayout.PropertyField (miscRoomAmm, true);	

		}
		EditorGUI.indentLevel--;


		SerializedProperty offsetSpace = serializedObject.FindProperty ("offsetSpace");
		EditorGUILayout.PropertyField (offsetSpace, true);	

		if (EditorGUI.EndChangeCheck ())
			serializedObject.ApplyModifiedProperties ();

		SerializedProperty totalRoom = serializedObject.FindProperty ("totalRoom");
		EditorGUILayout.PropertyField (totalRoom, true);	
	}




}
