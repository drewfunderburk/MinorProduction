﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileSpawnerBehaviour : MonoBehaviour
{
    [Tooltip("Positions to fire projectiles from")]
    [SerializeField] private Transform[] _spawnPositions = null;

    [Tooltip("Projectile to be fired")]
    [SerializeField] private GameObject _projectilePrefab = null;

    [Tooltip("Time in seconds to delay between shots")]
    [SerializeField] private float _fireDelay = 1;

    [Tooltip("How much damage the projectiles will do")]
    [SerializeField] private float _damage;

    public Transform[] SpawnPositions { get => _spawnPositions; set => _spawnPositions = value; }
    public GameObject ProjectilePrefab { get => _projectilePrefab; set => _projectilePrefab = value; }
    public float FireDelay { get => _fireDelay; set => _fireDelay = value; }
    public float Damage { get => _damage; set => _damage = value; }

    private float _lastFireTime = 0;

    /// <summary>
    /// Fire a projectile from each SpawnPosition
    /// </summary>
    /// <returns>A list of projectiles fired</returns>
    public virtual List<BulletBehaviour> Fire()
    {
        // Ensure adequate time has passed to fire a shot
        if (Time.time <= _lastFireTime + FireDelay)
            return null;

        // Update the timer
        _lastFireTime = Time.time;

        List<BulletBehaviour> projectileList = new List<BulletBehaviour>();

        // Loop through all spawn positions and spawn a projectile there
        for (int i = 0; i < SpawnPositions.Length; i++)
        {
            BulletBehaviour projectile = Instantiate(_projectilePrefab, SpawnPositions[i].position, SpawnPositions[i].rotation).GetComponent<BulletBehaviour>();
            projectile.Damage = Damage;
            projectileList.Add(projectile);
        }

        return projectileList;
    }
}
