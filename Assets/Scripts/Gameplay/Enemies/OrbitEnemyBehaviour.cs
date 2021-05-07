using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PursueTargetBehaviour))]
[RequireComponent(typeof(OrbitTargetAtDistanceBehaviour))]
public class OrbitEnemyBehaviour : MonoBehaviour
{
    public Transform Target;

    [Tooltip("Distance at which to switch from Pursue to Orbit")]
    [SerializeField] private float _transitionDistance = 15;

    private PursueTargetBehaviour _pursueBehaviour;
    private OrbitTargetAtDistanceBehaviour _orbitBehaviour;

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
}
