using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]
public class ArriveAtPointBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition = new Vector3();

    /// <summary>
    /// Position to move to
    /// </summary>
    public Vector3 TargetPosition
    {
        get { return _targetPosition; }
        set
        {
            // Update value
            _targetPosition = value;

            // Find a new path
            _needsPath = true;
        }
    }
    
    // Reference to the NavMeshAgent for pathfinding
    private NavMeshAgent _agent;
    private bool _needsPath = true;

    private void OnDisable()
    {
        // Reset the path on disabling this script so the agent doesn't keep running after a target when it shouldn't
        _agent?.ResetPath();
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // If a new path is needed
        if (_needsPath)
        {
            // Calculate a new path
            _agent.SetDestination(TargetPosition);

            // A new path is no longer needed
            _needsPath = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!enabled)
            return;

        // If the agent has a path
        if (_agent && _agent.hasPath)
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

        // Draw a wire sphere and a label at TargetPosition
        Handles.Label(TargetPosition, "Target Position");
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(TargetPosition, 0.5f);
    }
}

[CustomEditor(typeof(ArriveAtPointBehaviour))]
class ArriveAtPointBehaviourEditor : Editor
{
    // Whether or not the help text should be shown
    private bool _showText = true;

    public override void OnInspectorGUI()
    {
        // Get reference to script
        ArriveAtPointBehaviour script = target as ArriveAtPointBehaviour;

        // Declare help text
        string helpText = "While this script is enabled, it will use the attached Nav Mesh Agent component to allow this GameObject to pathfind to a designated point in space.";

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