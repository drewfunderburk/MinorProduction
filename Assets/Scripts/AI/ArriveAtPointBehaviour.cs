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
    public Vector3 Target
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
        _agent.ResetPath();
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
            _agent.SetDestination(Target);

            // A new path is no longer needed
            _needsPath = false;
        }
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