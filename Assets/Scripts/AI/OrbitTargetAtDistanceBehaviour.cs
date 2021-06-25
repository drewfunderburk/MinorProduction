using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody))]
public class OrbitTargetAtDistanceBehaviour : MonoBehaviour
{
    [Tooltip("Target to orbit")]
    [SerializeField] private Transform _target;
    public Transform Target { get => _target; set => _target = value; }

    [Tooltip("How many points around the target should this agent use for pathfinding. More points means a more accurate circle.")]
    [Min(3)]
    [SerializeField] private int _numberOfPoints = 10;
    public int NumberOfPoints { get => _numberOfPoints; set => _numberOfPoints = value; }

    [Tooltip("The distance at which the agent should orbit.")]
    [SerializeField] private float _orbitDistance = 10;
    public float OrbitDistance { get => _orbitDistance; set => _orbitDistance = value; }

    [Tooltip("How close the agent must come to the points on the circle to be considered \'arrived\'")]
    [SerializeField] private float _pointTolerance = 0.5f;

    [Tooltip("How fast to approach orbit points")]
    [SerializeField] private float _speed = 1;
    public float Speed { get => _speed; set => _speed = value; }

    private Rigidbody _rigidbody;
    private Vector3[] _pathPoints;
    private int _destinationPoint = 0;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = false;
        if (Target)
        {
            // Calculate orbit points and set destination to the closest one
            _pathPoints = GetPointsOnCircle();
            SetDestinationToClosestPoint();
        }
    }

    private void OnDisable()
    {
        _rigidbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (Target == null)
            return;

        _pathPoints = GetPointsOnCircle();

        float distanceToNextPoint = Vector3.Distance(transform.position, _pathPoints[_destinationPoint]);
        if (distanceToNextPoint <= _pointTolerance)
            IncrementPoint();

        Vector2 position = new Vector2(transform.position.x, transform.position.z);
        Vector2 destination = new Vector2(_pathPoints[_destinationPoint].x, _pathPoints[_destinationPoint].z);
        Vector2 direction2 = (destination - position).normalized;
        Vector3 direction = new Vector3(direction2.x, 0, direction2.y);

        _rigidbody.velocity = direction * Speed * Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (!enabled)
            return;
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

    private void SetDestinationToClosestPoint()
    {
        float shortestDistance = -1;
        int pointIndex = -1;

        // Loop through all path points and determine the point with the shortest distance from the AI
        for (int i = 0; i < _pathPoints.Length; i++)
        {
            // Set the first point to be the shortest if none has been set yet
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
        // Increment to the next point in the array, wrapping around to zero
        _destinationPoint = (_destinationPoint + 1) % NumberOfPoints;
    }

    private Vector3[] GetPointsOnCircle()
    {
        // Array to hold points
        Vector3[] pointArray = new Vector3[NumberOfPoints];

        // Store angle in radians between each point
        float angleIncrement = (2 * Mathf.PI) / NumberOfPoints;
        float angle = 0;

        // Create points evenly spaced around the object
        for (int i = 0; i < NumberOfPoints; i++)
        {
            // Get point on unit circle
            pointArray[i] = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

            // Scale point up to orbit distance
            pointArray[i] *= OrbitDistance;

            // Offset point to target position
            pointArray[i] += Target.position;

            angle += angleIncrement;
        }

        return pointArray;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(OrbitTargetAtDistanceBehaviour))]
class OrbitTargetAtDistanceBehaviourEditor : Editor
{
    // Whether or not the help text should be shown
    private bool _showText = true;

    public override void OnInspectorGUI()
    {
        // Get reference to script
        OrbitTargetAtDistanceBehaviour script = target as OrbitTargetAtDistanceBehaviour;

        // Declare help text
        string helpText = "While this script is enabled, it will use the attached NavMeshAgent to orbit its target at a specified distance.";

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