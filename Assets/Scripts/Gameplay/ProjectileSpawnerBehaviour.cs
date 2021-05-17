using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileSpawnerBehaviour
{
    [Tooltip("Positions to fire projectiles from")]
    [SerializeField] private Transform[] _spawnPositions = null;
    public Transform[] SpawnPositions { get => _spawnPositions; set => _spawnPositions = value; }

    [Tooltip("Projectile to be fired")]
    [SerializeField] private GameObject _projectilePrefab;
    public GameObject ProjectilePrefab { get => _projectilePrefab; set => _projectilePrefab = value; }

    [Tooltip("Time in seconds to delay between shots")]
    [SerializeField] private float _fireDelay = 1;
    public float FireDelay { get => _fireDelay; set => _fireDelay = value; }

    private float _lastFireTime = 0;

    /// <summary>
    /// Fire a projectile from each SpawnPosition
    /// </summary>
    /// <returns>A list of projectiles fired</returns>
    public virtual List<GameObject> Fire()
    {
        // Ensure adequate time has passed to fire a shot
        if (_lastFireTime <= Time.time + FireDelay)
            return null;

        // Update the timer
        _lastFireTime = Time.time;

        List<GameObject> projectileList = new List<GameObject>();

        // Loop through all spawn positions and spawn a projectile there
        for (int i = 0; i < SpawnPositions.Length; i++)
        {
            GameObject projectile = Object.Instantiate(_projectilePrefab, SpawnPositions[i].position, SpawnPositions[i].rotation);
            projectileList.Add(projectile);
        }

        return projectileList;
    }
}
