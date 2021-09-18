using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MultiplePathGenerator))]
public class MultiPathGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MultiplePathGenerator pathGenerator = (MultiplePathGenerator)target;
        if (GUILayout.Button("Generate Multi Path"))
        {
            pathGenerator.GenerateMultiPath();
        }
    }
}
