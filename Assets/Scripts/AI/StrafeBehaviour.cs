using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StrafeBehaviour : MonoBehaviour
{
    [Tooltip("Padding from the sides of the screen")]
    [SerializeField] private float _straftPadding = 0;

    private Vector3 _leftPosition = new Vector3();
    private Vector3 _rightPosition = new Vector3();
    private Vector3 _destination;
    private NavMeshAgent _agent;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();

        // Don't update the agent's rotation so that it can be controlled by a shoot behaviour
        _agent.updateRotation = false;
        SetStrafePositions();
        SetDestinationToClosestPoint();
    }
    private void OnDisable()
    {
        _agent.updateRotation = true;
    }

    private void Update()
    {
        // If we've arrived at a point, strafe towards the other
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            ToggleDestinationPosition();
    }

    private void OnDrawGizmosSelected()
    {
        if (!enabled)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_leftPosition, 0.5f);
        Gizmos.DrawWireSphere(_rightPosition, 0.5f);
        Gizmos.DrawLine(_leftPosition, _rightPosition);
    }

    /// <summary>
    /// Set the left and right strafe positions for this object based on it's z position
    /// </summary>
    private void SetStrafePositions()
    {
        // Cache camera
        Camera cam = Camera.main;

        // Get screen positions
        Vector3 leftScreenPosition = new Vector3(0, 0, cam.transform.position.y);
        Vector3 rightScreenPosition = new Vector3(Screen.width, Screen.height, cam.transform.position.y);

        // Convert to world position
        Vector3 leftPosition = cam.ScreenToWorldPoint(leftScreenPosition);
        Vector3 rightPosition = cam.ScreenToWorldPoint(rightScreenPosition);

        // Apply Z Position
        leftPosition.z = transform.position.z;
        rightPosition.z = transform.position.z;

        // Clamp Y Position
        leftPosition.y = 0;
        rightPosition.y = 0;
        // Apply padding
        leftPosition.x += _straftPadding;
        rightPosition.x -= _straftPadding;

        // Get positions on navmesh
        NavMeshHit leftWorldPosition;
        NavMesh.SamplePosition(leftPosition, out leftWorldPosition, _agent.height * 2, NavMesh.AllAreas);
        NavMeshHit rightWorldPosition;
        NavMesh.SamplePosition(rightPosition, out rightWorldPosition, _agent.height * 2, NavMesh.AllAreas);

        // Set member variables
        _leftPosition = leftWorldPosition.position;
        _rightPosition = rightWorldPosition.position;
    }

    /// <summary>
    /// Set the destination to the left or right point based on which is closer
    /// </summary>
    private void SetDestinationToClosestPoint()
    {
        // Get distances
        float distanceToLeftPosition = Vector3.Distance(transform.position, _leftPosition);
        float distanceToRightPosition = Vector3.Distance(transform.position, _rightPosition);

        // If the distance to the left position is higher than the right, set the destination to the right and vice versa
        _destination = distanceToLeftPosition > distanceToRightPosition ? _rightPosition : _leftPosition;
        _agent.SetDestination(_destination);
    }

    /// <summary>
    /// Toggles the destination to the other point
    /// </summary>
    private void ToggleDestinationPosition()
    {
        // If the destination is set to the left position, swap it to right and vice versa
        _destination = _destination == _leftPosition ? _rightPosition : _leftPosition;
        _agent.SetDestination(_destination);
    }
}
