using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(PursueTargetBehaviour))]
public class RamEnemyBehaviour : MonoBehaviour
{
    public Transform Target;

    private NavMeshAgent _agent;
    private PursueTargetBehaviour _pursueTargetBehaviour;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _pursueTargetBehaviour = GetComponent<PursueTargetBehaviour>();
        _pursueTargetBehaviour.Target = Target;
    }
}

[CustomEditor(typeof(RamEnemyBehaviour))]
class RamEnemyBehaviourEditor : Editor
{
    // Whether or not the help text should be shown
    private bool _showText = true;

    public override void OnInspectorGUI()
    {
        // Get reference to script
        RamEnemyBehaviour script = target as RamEnemyBehaviour;

        // Declare help text
        string helpText = "This enemy type will use a PursueTargetBehaviour to run down a target and collide with it";

        // Display help text
        _showText = EditorGUILayout.BeginFoldoutHeaderGroup(_showText, "Info");
        if (_showText)
        {
            EditorGUILayout.HelpBox(helpText, MessageType.Info);
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        // Display base inspector gui
        base.OnInspectorGUI();
    }
}