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
        if (Target.position == _previousTargetPosition && _agent.hasPath)
            return;

        // Update previous target position
        _previousTargetPosition = Target.position;

        // Update NavMeshAgent's destination
        _agent?.SetDestination(Target.position);
    }

    private void OnDrawGizmosSelected()
    {
        if (!enabled)
            return;
        if (_agent == null)
            return;

        // If the agent has a path
        if (_agent.hasPath)
        {
            // Draw a line between each of the path corners
            Gizmos.color = Color.green;
            Vector3[] path = _agent.path.corners;
            for (int i = 0; i < path.Length; i++)
            {
                if (i == 0)
                    Gizmos.DrawLine(transform.position, path[i]);
                else
                    Gizmos.DrawLine(path[i - 1], path[i]);
            }
        }
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