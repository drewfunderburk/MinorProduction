using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ArriveAtPointBehaviour))]
[RequireComponent(typeof(AIShootBehaviour))]
[RequireComponent(typeof(LookAtTargetBehaviour))]
[RequireComponent(typeof(NavMeshAgent))]
public class StationaryEnemyBehaviour : EnemyBehaviour
{
    enum State
    {
        ARRIVE,
        SHOOT
    }

    [Tooltip("Left position padding")]
    [SerializeField] [Range(0, 1)] private float _leftPadding = 0;
    [Tooltip("Right position padding")]
    [SerializeField] [Range(0, 1)] private float _rightPadding = 0;
    [Tooltip("Top position padding")]
    [SerializeField] [Range(0, 1)] private float _topPadding = 0;
    [Tooltip("Bottom position padding")]
    [SerializeField] [Range(0, 1)] private float _bottomPadding = 0;

    [Tooltip("Median time in seconds to wait at position before finding another")]
    [SerializeField] private float _newPositionMedianDelay = 7;

    [Tooltip("Positive and negative offset in seconds from median delay")]
    [SerializeField] private float _delayOffset = 3;

    private ArriveAtPointBehaviour _arriveBehaviour;
    private AIShootBehaviour _shootBehaviour;
    private LookAtTargetBehaviour _lookAtTarget;
    private NavMeshAgent _agent;

    private float _newPositionTimer = 0;
    private float _newPositionDelay;

    private State _currentState = State.ARRIVE;

    private void Start()
    {
        _arriveBehaviour = GetComponent<ArriveAtPointBehaviour>();
        _shootBehaviour = GetComponent<AIShootBehaviour>();
        _lookAtTarget = GetComponent<LookAtTargetBehaviour>();
        _agent = GetComponent<NavMeshAgent>();

        _shootBehaviour.enabled = false;
        _lookAtTarget.enabled = false;

        _lookAtTarget.Target = Target;
        _arriveBehaviour.TargetPosition = GetRandomPositionInView();

        _newPositionDelay = Random.Range(_newPositionMedianDelay - _delayOffset, _newPositionMedianDelay + _delayOffset);
    }

    private void Update()
    {
        // Increment timer if we're currently shooting
        if (_currentState == State.SHOOT)
            _newPositionTimer += Time.deltaTime;

        // If enough time has passed for a new position
        if (_newPositionTimer > _newPositionDelay)
        {
            // Reset timer
            _newPositionTimer = 0;
            _newPositionDelay = Random.Range(_newPositionMedianDelay - _delayOffset, _newPositionMedianDelay + _delayOffset);

            // Swap to arriveBehaviour
            _currentState = State.ARRIVE;
            _arriveBehaviour.enabled = true;
            _agent.enabled = true;
            _shootBehaviour.enabled = false;
            _lookAtTarget.enabled = false;

            // Set target
            _arriveBehaviour.TargetPosition = GetRandomPositionInView();
        }

        // If we have a path and we've arrived at our destination
        if (_agent.hasPath && _agent.remainingDistance < 0.5f)
        {
            // Switch to shootBehaviour
            _currentState = State.SHOOT;
            _arriveBehaviour.enabled = false;
            _agent.enabled = false;
            _lookAtTarget.enabled = true;
            _shootBehaviour.enabled = true;
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
        float xScreenPos = Random.Range(0 + (Screen.width * _leftPadding) , Screen.width - (Screen.width * _rightPadding));
        float yScreenPos = Random.Range(0 + (Screen.height * _bottomPadding) , Screen.height - (Screen.height * _topPadding));

        // Get screen position with world y of zero
        Vector3 position = new Vector3(xScreenPos, yScreenPos, cam.transform.position.y);

        // Convert to world position
        Vector3 screenPosition = cam.ScreenToWorldPoint(position);
        screenPosition.y = 0;

        // Get position on navmesh
        NavMeshHit hit;
        NavMesh.SamplePosition(screenPosition, out hit, _agent.height * 2, NavMesh.AllAreas);

        return hit.position;
    }
}
