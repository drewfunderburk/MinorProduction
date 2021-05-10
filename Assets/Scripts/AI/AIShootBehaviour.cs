using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIShootBehaviour : MonoBehaviour
{
    [Tooltip("Object to spawn as a projectile")]
    [SerializeField] private GameObject _bulletPrefab;

    [Tooltip("Where to fire the projectile from")]
    [SerializeField] private Vector3[] _fireLocations;
    public Vector3[] FireLocations { get { return _fireLocations; } }

    [Tooltip("Delay in seconds between shots")]
    [SerializeField] private float _fireDelay = 1f;

    private float _fireTimer = 0;

    private void Update()
    {
        // Increment timer
        _fireTimer += Time.deltaTime;

        // If time has passed for a new shot
        if (_fireTimer > _fireDelay)
        {
            // Reset timer
            _fireTimer = 0;

            for (int i = 0; i < _fireLocations.Length; i++)
            {
                Vector3 position = (transform.position + transform.forward) + _fireLocations[i];
                GameObject bullet = Instantiate(_bulletPrefab, position, Quaternion.identity);
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
            Handles.Label((transform.position + transform.forward) + _fireLocations[i], "Fire location " + i);
            Gizmos.DrawWireSphere((transform.position + transform.forward) + _fireLocations[i], 0.2f);
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

        // Create position handles for each of the Fire Locations to allow them to be moved in the scene view
        for (int i = 0; i < script.FireLocations.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newPosition = Handles.PositionHandle((script.transform.localPosition + script.transform.forward) + script.FireLocations[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
                script.FireLocations[i] = (newPosition - script.transform.position - script.transform.forward);
        }
    }
}
