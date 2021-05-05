using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
