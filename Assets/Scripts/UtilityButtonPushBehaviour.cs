using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

/// <summary>
/// This exists as a utility to make testing in scene easier
/// </summary>
public class UtilityButtonPushBehaviour : MonoBehaviour
{
    public UnityEvent ButtonPush;
}

[CustomEditor(typeof(UtilityButtonPushBehaviour))]
class UtilityButtonPushBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This prefab exists to give an event and a button push to help test things in scene.", MessageType.Info);
        
        base.OnInspectorGUI();

        UtilityButtonPushBehaviour script = target as UtilityButtonPushBehaviour;

        if (GUILayout.Button("Invoke Button Push"))
            script.ButtonPush.Invoke();
    }
}