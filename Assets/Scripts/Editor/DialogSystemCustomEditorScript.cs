using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogSystemManager))]
public class DialogSystemCustomEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogHolder"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonParent"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("characterImageObject"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("characterName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogText"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogStructList"));

        serializedObject.ApplyModifiedProperties();
    }
}
