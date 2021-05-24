using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LookAtTargetBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public Transform Target { get => _target; set => _target = value; }

    [SerializeField] private float _rotationSpeed = 1;

    private NavMeshAgent _agent;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    private void OnDisable()
    {
        // Give rotation control back to the NavMeshAgent
        _agent.updateRotation = true;
    }

    private void Update()
    {
        // Rotate to look at target
        transform.LookAt(Target);
    }
}
