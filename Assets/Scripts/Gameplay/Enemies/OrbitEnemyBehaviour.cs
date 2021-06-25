using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PursueTargetBehaviour))]
[RequireComponent(typeof(OrbitTargetAtDistanceBehaviour))]
[RequireComponent(typeof(AIShootBehaviour))]
[RequireComponent(typeof(LookAtTargetBehaviour))]
public class OrbitEnemyBehaviour : EnemyBehaviour
{
    public override Transform Target 
    { 
        get => base.Target;
        set
        {
            base.Target = value;
            _pursueBehaviour.Target = base.Target;
            _orbitBehaviour.Target = base.Target;
            _shootBehaviour.Target = base.Target;
        }
    }

    [Tooltip("Distance at which to switch from Pursue to Orbit")]
    [SerializeField] private float _transitionDistance = 15;

    private NavMeshAgent _agent;
    private PursueTargetBehaviour _pursueBehaviour;
    private OrbitTargetAtDistanceBehaviour _orbitBehaviour;
    private AIShootBehaviour _shootBehaviour;
    private LookAtTargetBehaviour _lookAtTarget;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _pursueBehaviour = GetComponent<PursueTargetBehaviour>();
        _orbitBehaviour = GetComponent<OrbitTargetAtDistanceBehaviour>();
        _shootBehaviour = GetComponent<AIShootBehaviour>();
        _lookAtTarget = GetComponent<LookAtTargetBehaviour>();

        _pursueBehaviour.Target = Target;
        _orbitBehaviour.Target = Target;
        _lookAtTarget.Target = Target;

        // Set orbit speed to be the NavMeshAgent speed for consistency
        _orbitBehaviour.Speed = _agent.speed * 50;
    }

    private void Update()
    {
        // If we are within orbit range, begin orbiting
        if (Vector3.Distance(transform.position, Target.position) < _transitionDistance)
        {
            _pursueBehaviour.enabled = false;
            _agent.ResetPath();
            _orbitBehaviour.enabled = true;
            _shootBehaviour.enabled = true;
            _lookAtTarget.enabled = true;
        }
        // Otherwise, pursue towards the target again
        else
        {
            _pursueBehaviour.enabled = true;
            _orbitBehaviour.enabled = false;
            _shootBehaviour.enabled = false;
            _lookAtTarget.enabled = false;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(OrbitEnemyBehaviour))]
class OrbitEnemyBehaviourEditor : Editor
{
    // Whether or not the help text should be shown
    private bool _showText = true;

    public override void OnInspectorGUI()
    {
        // Get reference to script
        OrbitEnemyBehaviour script = target as OrbitEnemyBehaviour;

        // Declare help text
        string helpText = "This enemy will use a PursueTargetBehaviour to close the distance to its target before switching to an OrbitTargetAtDistanceBehaviour to orbit.";

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
#endif