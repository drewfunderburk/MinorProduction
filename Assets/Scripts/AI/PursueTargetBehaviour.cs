using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]
public class PursueTargetBehaviour : MonoBehaviour
{
    /// <summary>
    /// Target to pursue
    /// </summary>
    public Transform Target;

    // Reference to the NavMeshAgent for pathfinding
    private NavMeshAgent _agent;

    // Last place the target was to ensure we don't calculate a path if the target hasn't moved
    private Vector3 _previousTargetPosition;

    private void OnDisable()
    {
        // Reset the path on disabling this script so the agent doesn't keep running after a target when it shouldn't
        _agent.ResetPath();
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Ensure Target exists
        if (Target == null)
            return;

        // Ensure Target has moved
        if (Target.position == _previousTargetPosition)
            return;

        // Update previous target position
        _previousTargetPosition = Target.position;

        // Update NavMeshAgent's destination
        _agent.SetDestination(Target.position);
    }
}


[CustomEditor(typeof(PursueTargetBehaviour))]
class PursueTargetBehaviourEditor : Editor
{
    // Whether or not the help text should be shown
    private bool _showText = true;

    public override void OnInspectorGUI()
    {
        // Get reference to script
        PursueTargetBehaviour script = target as PursueTargetBehaviour;

        // Declare help text
        string helpText = "While this script is enabled, it will use the attached Nav Mesh Agent component to allow this GameObject to pathfind to a destination.";

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