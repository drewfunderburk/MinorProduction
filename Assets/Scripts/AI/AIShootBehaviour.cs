using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIShootBehaviour : MonoBehaviour
{
    [Tooltip("What to shoot at. Leave null to fire straight ahead.")]
    [SerializeField] private Transform _target;
    public Transform Target { get => _target; set => _target = value; }

    [Tooltip("Object to spawn as a projectile")]
    [SerializeField] private GameObject _bulletPrefab = null;

    [Tooltip("Where to fire the projectile from")]
    [SerializeField] private Vector3[] _fireLocations = null;
    public Vector3[] FireLocations { get { return _fireLocations; } }


    [Tooltip("Delay in seconds between shots")]
    [SerializeField] private float _fireDelay = 1f;

    private float _fireTimer = 0;

    private Vector3[] _fireLocationsWithOffset = null;
    public Vector3[] FireLocationsWithOffset { get => _fireLocationsWithOffset; private set => _fireLocationsWithOffset = value; }

    private void UpdateFireLocationsWithOffset()
    {
        for (int i = 0; i < FireLocationsWithOffset.Length; i++)
        {
            // Get relative offset position
            Vector3 x = transform.right * FireLocations[i].x;
            Vector3 y = transform.up * FireLocations[i].y;
            Vector3 z = transform.forward * FireLocations[i].z;
            FireLocationsWithOffset[i] = x + y + z;
        }
    }

    private void Start()
    {
        FireLocationsWithOffset = new Vector3[_fireLocations.Length];
    }

    private void Update()
    {
        // Increment timer
        _fireTimer += Time.deltaTime;

        UpdateFireLocationsWithOffset();

        // If time has passed for a new shot
        if (_fireTimer > _fireDelay)
        {
            // Reset timer
            _fireTimer = 0;

            for (int i = 0; i < FireLocationsWithOffset.Length; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab, FireLocationsWithOffset[i], Quaternion.identity);

                if (Target != null)
                    bullet.transform.LookAt(Target);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!enabled)
            return;

        // Draw wire spheres at each of the Fire Locations
        for (int i = 0; i < _fireLocations.Length; i++)
        {
            Gizmos.color = Color.green;
            Handles.Label(FireLocationsWithOffset[i], "Fire location " + i);
            Gizmos.DrawWireSphere(FireLocationsWithOffset[i], 0.2f);
        }
    }
}

[CustomEditor(typeof(AIShootBehaviour))]
public class AIShootBehaviourEditor : Editor
{
    private void OnSceneGUI()
    {
        AIShootBehaviour script = target as AIShootBehaviour;

        if (!script.enabled)
            return;
        if (script.FireLocations == null)
            return;

        // Create position handles for each of the Fire Locations to allow them to be moved in the scene view
        for (int i = 0; i < script.FireLocationsWithOffset.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newPosition = Handles.PositionHandle(script.FireLocationsWithOffset[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
                script.FireLocations[i] = (newPosition - script.transform.position);
        }
    }
}
