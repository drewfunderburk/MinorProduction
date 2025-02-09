﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawnerBehaviour : MonoBehaviour
{
    [SerializeField] private DifficultyCurvesScriptableObject _difficultyCurves = null;

    [Tooltip("Should enemies be allowed to spawn?")]
    [SerializeField] private bool _allowSpawning = true;
    public bool AllowSpawning { get => _allowSpawning; set => _allowSpawning = value; }

    [Tooltip("Enemies to spawn")]
    [SerializeField] private GameObject[] _enemyPrefabs = null;

    [Tooltip("Target to pass to enemies")]
    [SerializeField] private Transform _target = null;

    [Tooltip("Width of spawn area")]
    [SerializeField] private float _spawnWidth = 100;

    [Tooltip("Percentage of the wave over which to spawn enemies")]
    [SerializeField] [Range(0, 1)] private float _waveSpawnPercentage = 0.5f;

    // How long between each individual spawn in a wave
    private float _enemySpawnDelay = 0.5f;

    private List<EnemyBehaviour> _enemies = new List<EnemyBehaviour>();

    // Default values for difficulty curves
    private int _spawnCount = 5;
    private float _spawnDelay = 3;
    private float _delayBetweenWaveGroups = 3;
    private float _wavesInGroup = 1;

    private float _spawnTimer = 0;
    private int _waveNumber = 0;
    private Vector3 _spawnLocation = new Vector3();


    private void Start()
    {
        EvaluateDifficultyCurves();

        _spawnTimer = _spawnDelay;
        _enemies = new List<EnemyBehaviour>();
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (AllowSpawning && _spawnTimer > _spawnDelay)
        {
            _spawnTimer = 0;
            EvaluateDifficultyCurves();

            // If _waveNumber is nonzero and a multiple of _wavesInGroup, increase delay
            if (_waveNumber != 0 && _waveNumber % (_wavesInGroup - 1) == 0)
                _spawnTimer -= _delayBetweenWaveGroups;

            _enemySpawnDelay = (_spawnDelay / _spawnCount) * _waveSpawnPercentage;
            SpawnRandomIndexWave();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw spawn width 
        Gizmos.color = Color.green;
        Vector3 start = new Vector3(-_spawnWidth, transform.position.y, transform.position.z);
        Vector3 end = new Vector3(_spawnWidth, transform.position.y, transform.position.z);
        Gizmos.DrawLine(start, end);
    }

    public void ResetTimer()
    {
        _spawnTimer = _spawnDelay;
    }

    /// <summary>
    /// Destroys all enemies in the scene and clears the list
    /// </summary>
    public void ClearEnemies()
    {
        foreach (EnemyBehaviour enemy in _enemies)
        {
            // Ensure enemy is not null
            if (enemy == null)
                continue;

            // If enemy has a projectile spawner, destroy all of it's bullets
            ProjectileSpawnerBehaviour spawner = enemy.GetComponent<ProjectileSpawnerBehaviour>();
            if (spawner)
                spawner.ClearProjectiles();

            Destroy(enemy.gameObject);
        }

        _enemies.Clear();
    }

    /// <summary>
    /// Choose a random x between -spawnWidth and spawnWidth. Set spawnLocation to that point with the same y and z as this object
    /// </summary>
    private void RandomizeSpawnLocation()
    {
        _spawnLocation = new Vector3(Random.Range(-_spawnWidth, _spawnWidth), transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Spawns an enemy from EnemyPrefabs at a given index
    /// </summary>
    private void SpawnEnemyAtIndex(int index)
    {
        GameObject enemy = Instantiate(_enemyPrefabs[index], _spawnLocation, Quaternion.identity);

        EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
        if (enemyBehaviour)
            enemyBehaviour.Target = _target;

        _enemies.Add(enemyBehaviour);
    }

    /// <summary>
    /// Selects a random index and spawns a wave of that enemy
    /// </summary>
    private void SpawnRandomIndexWave()
    {
        int index = Random.Range(0, _enemyPrefabs.Length);
        RandomizeSpawnLocation();
        StartCoroutine(SpawnIndexWave(index, _spawnCount));
    }

    /// <summary>
    /// Spawns a random enemy from EnemyPrefabs
    /// </summary>
    private void SpawnRandomEnemy()
    {
        if (_enemyPrefabs.Length == 1)
        {
            SpawnEnemyAtIndex(0);
            return;
        }
        int index = Random.Range(0, _enemyPrefabs.Length);
        SpawnEnemyAtIndex(index);
    }

    /// <summary>
    /// Spawns a wave of enemies from EnemyPrefabs at a given index
    /// </summary>
    /// <param name="index">Which enemy to spawn</param>
    /// <param name="numEnemies">How many to spawn</param>
    private IEnumerator SpawnIndexWave(int index, int numEnemies)
    {
        _waveNumber++;
        for (int i = 0; i < numEnemies; i++)
        {
            SpawnEnemyAtIndex(index);
            yield return new WaitForSeconds(_enemySpawnDelay);
        }
    }

    /// <summary>
    /// Spawns a wave of random enemies from EnemyPrefabs
    /// </summary>
    /// <param name="numEnemies">How many to spawn</param>
    private IEnumerator SpawnRandomWave(int numEnemies)
    {
        _waveNumber++;
        for (int i = 0; i < numEnemies; i++)
        {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(_enemySpawnDelay);
        }
    }

    /// <summary>
    /// Evaluate difficulty curves from GameManagerBehaviour and cache them
    /// </summary>
    private void EvaluateDifficultyCurves()
    {
        float time = (float)GameManagerBehaviour.Instance.Level / GameManagerBehaviour.Instance.MaxLevel;

        _spawnDelay = _difficultyCurves.EnemySpawnDelay.Evaluate(time);
        _spawnCount = Mathf.RoundToInt(_difficultyCurves.EnemySpawnCount.Evaluate(time));
        _delayBetweenWaveGroups = _difficultyCurves.DelayBetweenWaveGroups.Evaluate(time);
        _wavesInGroup = Mathf.RoundToInt(_difficultyCurves.WavesInGroup.Evaluate(time));
    }
}
