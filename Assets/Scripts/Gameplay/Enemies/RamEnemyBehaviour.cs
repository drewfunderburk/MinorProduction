using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(PursueTargetBehaviour))]
public class RamEnemyBehaviour : EnemyBehaviour
{
    public override Transform Target 
    { 
        get => base.Target;
        set
        {
            base.Target = value;
            _pursueTargetBehaviour.Target = base.Target;
        }
    }

    [Tooltip("How close the enemy must be to the player before it loses acceleration")]
    [SerializeField] private float _accelerationReductionDistance = 5;

    private NavMeshAgent _agent;
    private PursueTargetBehaviour _pursueTargetBehaviour;
    private float _initialAcceleration;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _pursueTargetBehaviour = GetComponent<PursueTargetBehaviour>();
        _pursueTargetBehaviour.Target = Target;
    }

    private void Start()
    {
        _initialAcceleration = _agent.acceleration;
    }

    private void Update()
    {
        // Get distance to target
        float distance = Vector3.Distance(transform.position, Target.position);

        if (distance < _accelerationReductionDistance)
        {
            float accellerationPercentage = distance / _accelerationReductionDistance;
            _agent.acceleration = _initialAcceleration * accellerationPercentage;
        }
        else
            _agent.acceleration = _initialAcceleration;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Target.position, _accelerationReductionDistance);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Destroy(this.gameObject);
    }
}

#if UNITY_EDITOR
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
#endif