using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AutoPathGenerator))]
public class AutoPathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutoPathGenerator pathGenerator = (AutoPathGenerator)target;
        if(GUILayout.Button("Generate Path"))
        {
            pathGenerator.GeneratePath();
        }
    }
}
