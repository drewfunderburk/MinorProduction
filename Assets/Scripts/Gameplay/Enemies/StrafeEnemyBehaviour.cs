using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ArriveAtPointBehaviour))]
[RequireComponent(typeof(StrafeBehaviour))]
[RequireComponent(typeof(AIShootBehaviour))]
[RequireComponent(typeof(LookAtTargetBehaviour))]
public class StrafeEnemyBehaviour : EnemyBehaviour
{
    enum State
    {
        ARRIVE,
        STRAFE
    }

    private State _currentState = State.ARRIVE;
    private ArriveAtPointBehaviour _arriveBehaviour;
    private StrafeBehaviour _strafeBehaviour;
    private AIShootBehaviour _shootBehaviour;
    private LookAtTargetBehaviour _lookAtTarget;
    private NavMeshAgent _agent;

    private void Start()
    {
        _arriveBehaviour = GetComponent<ArriveAtPointBehaviour>();
        _strafeBehaviour = GetComponent<StrafeBehaviour>();
        _shootBehaviour = GetComponent<AIShootBehaviour>();
        _lookAtTarget = GetComponent<LookAtTargetBehaviour>();
        _agent = GetComponent<NavMeshAgent>();

        _strafeBehaviour.enabled = false;
        _shootBehaviour.enabled = false;
        _lookAtTarget.enabled = false;
        _arriveBehaviour.enabled = true;

        _lookAtTarget.Target = Target;
        _arriveBehaviour.TargetPosition = GetRandomPositionInView();
    }

    private void Update()
    {
        if (_currentState == State.ARRIVE)
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                _currentState = State.STRAFE;
                _arriveBehaviour.enabled = false;
                _shootBehaviour.enabled = true;
                _strafeBehaviour.enabled = true;
                _lookAtTarget.enabled = true;
            }
        }
    }

    /// <summary>
    /// Get a random position on the navmesh within the camera's view
    /// </summary>
    private Vector3 GetRandomPositionInView()
    {
        // Cache camera
        Camera cam = Camera.main;

        // Get x and y screen coordinates
        float xScreenPos = Random.Range(0, Screen.width);
        float yScreenPos = Random.Range(Screen.height / 2, Screen.height);

        // Get screen position with world y of zero
        Vector3 position = new Vector3(xScreenPos, yScreenPos, cam.transform.position.y);

        // Convert to world position
        Vector3 screenPosition = cam.ScreenToWorldPoint(position);

        // Get position on navmesh
        NavMeshHit hit;
        NavMesh.SamplePosition(screenPosition, out hit, _agent.height * 2, NavMesh.AllAreas);

        return hit.position;
    }
}
