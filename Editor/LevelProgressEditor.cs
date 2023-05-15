using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(LevelProgress))]
public class LevelProgressEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelProgress myTarget = (LevelProgress)target;

        if (GUILayout.Button("LevelUp"))
        {
            myTarget.ProgressLevel();
        }
    }
}

