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
    [SerializeField] private Vector3[] _fireLocationOffsets = null;
    public Vector3[] FireLocationOffsets { get { return _fireLocationOffsets; } set { _fireLocationOffsets = value; } }

    [Tooltip("Delay in seconds between shots")]
    [SerializeField] private float _fireDelay = 1f;

    private float _fireTimer = 0;

    private Vector3[] _fireLocations = null;
    public Vector3[] FireLocations { get => _fireLocations; private set => _fireLocations = value; }

    public void UpdateFireLocations()
    {
        FireLocations = new Vector3[FireLocationOffsets.Length];

        for (int i = 0; i < FireLocations.Length; i++)
        {
            // Get relative offset position
            Vector3 x = transform.right * FireLocationOffsets[i].x;
            Vector3 y = transform.up * FireLocationOffsets[i].y;
            Vector3 z = transform.forward * FireLocationOffsets[i].z;
            FireLocations[i] = x + y + z;
        }
    }

    private void Start()
    {
        UpdateFireLocations();
    }

    private void Update()
    {
        // Increment timer
        _fireTimer += Time.deltaTime;

        // If time has passed for a new shot
        if (_fireTimer > _fireDelay)
        {
            // Reset timer
            _fireTimer = 0;

            for (int i = 0; i < FireLocations.Length; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab, FireLocations[i], transform.rotation);

                if (Target != null)
                    bullet.transform.LookAt(Target);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!enabled)
            return;
        if (FireLocations == null)
            return;

        UpdateFireLocations();

        // Draw wire spheres at each of the Fire Locations
        for (int i = 0; i < FireLocations.Length; i++)
        {
            Gizmos.color = Color.green;
            Handles.Label(FireLocations[i], "Fire location " + i);
            Gizmos.DrawWireSphere(FireLocations[i], 0.2f);
        }
    }
}