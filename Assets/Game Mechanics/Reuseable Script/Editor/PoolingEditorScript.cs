using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoolingScript))]
public class PoolingEditorScript : Editor {

    public override void OnInspectorGUI() {

        base.OnInspectorGUI();

        PoolingScript myTarget = (PoolingScript)target;

        //Prefab Setup
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Prefab");
        myTarget.prefab = EditorGUILayout.ObjectField(myTarget.prefab, typeof(GameObject)) as GameObject;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        //Pool Target Setup
        EditorGUILayout.LabelField("Pool");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Find Pool With Tag");
        myTarget.findGameObjectWithTag = EditorGUILayout.Toggle(myTarget.findGameObjectWithTag);
        EditorGUILayout.EndHorizontal();

        if(myTarget.findGameObjectWithTag) {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Pool Name");
            myTarget.poolTagName = EditorGUILayout.TextField(myTarget.poolTagName);
            EditorGUILayout.EndHorizontal();

        } else {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Pool Object");
            myTarget.pool = EditorGUILayout.ObjectField(myTarget.pool, typeof(GameObject)) as GameObject;
            EditorGUILayout.EndHorizontal();

        }

    }

}
