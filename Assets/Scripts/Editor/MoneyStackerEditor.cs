using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
[CustomEditor(typeof(MoneyStacker))]
public class MoneyStackerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MoneyStacker myTarget = (MoneyStacker)target;

        if (GUILayout.Button("Spawn"))
        {
            myTarget.StackItems(3);
        }

        if (GUILayout.Button("Empty"))
        {
            myTarget.StartEmptyStack();
        }
    }
}
