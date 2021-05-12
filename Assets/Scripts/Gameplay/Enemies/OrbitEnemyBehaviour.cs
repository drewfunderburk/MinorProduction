﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(PursueTargetBehaviour))]
[RequireComponent(typeof(OrbitTargetAtDistanceBehaviour))]
public class OrbitEnemyBehaviour : EnemyBehaviour
{
    public Transform Target;

    [Tooltip("Distance at which to switch from Pursue to Orbit")]
    [SerializeField] private float _transitionDistance = 15;

    private PursueTargetBehaviour _pursueBehaviour;
    private OrbitTargetAtDistanceBehaviour _orbitBehaviour;

    public override void TakeDamage(float damage)
    {
        // Decrease health
        Health -= damage;

        // If health <= 0 destroy this object
        if (Health <= 0)
            Destroy(this.gameObject);
    }

    private void Start()
    {
        _pursueBehaviour = GetComponent<PursueTargetBehaviour>();
        _orbitBehaviour = GetComponent<OrbitTargetAtDistanceBehaviour>();

        _pursueBehaviour.Target = Target;
        _orbitBehaviour.Target = Target;
    }

    private void Update()
    {
        // If we are within orbit range, begin orbiting
        if (Vector3.Distance(transform.position, Target.position) < _transitionDistance)
        {
            _pursueBehaviour.enabled = false;
            _orbitBehaviour.enabled = true;
        }
        // Otherwise, pursue towards the target again
        else
        {
            _pursueBehaviour.enabled = true;
            _orbitBehaviour.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the collided object's layer is in _collideWith
        if (((1 << collision.gameObject.layer) & CollideWith) != 0)
        {
            // Do things to the collided object
        }
    }
}

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