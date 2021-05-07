using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class OrbitTargetAtDistanceBehaviour : MonoBehaviour
{
    [Tooltip("Target to orbit")]
    public Transform Target;

    [Tooltip("How many points around the target should this agent use for pathfinding. More points means a more accurate circle.")]
    [Min(3)]
    [SerializeField] private int _numberOfPoints = 10;

    [Tooltip("The distance at which the agent should orbit.")]
    [SerializeField] private float _orbitDistance = 10;

    [Tooltip("How close the agent must come to the points on the circle to be considered \'arrived\'")]
    [SerializeField] private float _pointTolerance = 0.5f;

    public float OrbitDistance { get; set; }

    private NavMeshAgent _agent;
    private Vector3[] _pathPoints;
    private int _destinationPoint = 0;

    private void OnEnable()
    {
        // Prevent the agent from autobraking as it approaches points to make the orbit smoother
        if (_agent)
            _agent.autoBraking = false;

        // Calculate orbit points and set destination to the closest one
        _pathPoints = GetPointsOnCircle();
        SetDestinationToClosestPoint();
    }

    private void OnDisable()
    {
        // Reenable autobraking for other behaviours
        _agent.autoBraking = true;

        _agent.ResetPath();
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        // Prevent the agent from autobraking as it approaches points to make the orbit smoother
        _agent.autoBraking = false;

        // Calculate orbit points and set destination to the closest one
        _pathPoints = GetPointsOnCircle();
        SetDestinationToClosestPoint();
    }

    private void Update()
    {
        // If the remaining distance to the next point is within tolerance 
        //  and the agent is not currently calculating a path, 
        //  go to the next point in the orbit
        if (!_agent.pathPending && _agent.remainingDistance <= _pointTolerance)
        {
            _pathPoints = GetPointsOnCircle();
            IncrementPoint();
        }
    }

    /*
    private void OnDrawGizmos()
    {
        if (_pathPoints == null)
            return;
        if (_pathPoints.Length == 0)
            return;

        Gizmos.color = Color.green;

        for (int i = 0; i < _pathPoints.Length; i++)
        {
            Gizmos.DrawWireSphere(_pathPoints[i], 1);
        }
    }
    */

    private void SetDestinationToClosestPoint()
    {
        float shortestDistance = -1;
        int pointIndex = -1;

        for (int i = 0; i < _pathPoints.Length; i++)
        {
            if (shortestDistance == -1)
            {
                shortestDistance = Vector3.Distance(transform.position, _pathPoints[i]);
                pointIndex = i;
                continue;
            }

            float distance = Vector3.Distance(transform.position, _pathPoints[i]);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                pointIndex = i;
            }
        }
        _destinationPoint = pointIndex;
    }

    private void IncrementPoint()
    {
        // Set the destination to the current point
        _agent.SetDestination(_pathPoints[_destinationPoint]);

        // Increment to the next point in the array, wrapping around to zero
        _destinationPoint = (_destinationPoint + 1) % _numberOfPoints;
    }

    private Vector3[] GetPointsOnCircle()
    {
        // Array to hold points
        Vector3[] pointArray = new Vector3[_numberOfPoints];

        // Store angle in radians between each point
        float angleIncrement = (2 * Mathf.PI) / _numberOfPoints;
        float angle = 0;

        // Create points evenly spaced around the object
        for (int i = 0; i < _numberOfPoints; i++)
        {
            // Get point on unit circle
            pointArray[i] = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

            // Scale point up to orbit distance
            pointArray[i] *= _orbitDistance;

            // Offset point to target position
            pointArray[i] += Target.position;

            angle += angleIncrement;
        }

        return pointArray;
    }
}
