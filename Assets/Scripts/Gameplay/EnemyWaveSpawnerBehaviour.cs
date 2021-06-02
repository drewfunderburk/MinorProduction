using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawnerBehaviour : MonoBehaviour
{
    [Tooltip("Should enemies be allowed to spawn?")]
    [SerializeField] private bool _allowSpawning = true;
    public bool AllowSpawning { get => _allowSpawning; set => _allowSpawning = value; }

    [Tooltip("Enemies to spawn")]
    [SerializeField] private GameObject[] _enemyPrefabs = null;

    [Tooltip("Target to pass to enemies")]
    [SerializeField] private Transform _target = null;

    // How long between each individual spawn in a wave
    private float _enemySpawnDelay = 0.5f;

    private List<EnemyBehaviour> _enemies = new List<EnemyBehaviour>();


    private int _spawnCount = 5;
    private float _spawnDelay = 3;
    private float _delayBetweenWaveGroups = 3;
    private float _wavesInGroup = 1;

    private float _spawnTimer = 0;
    private int _waveNumber = 0;

    /// <summary>
    /// Spawns an enemy from EnemyPrefabs at a given index
    /// </summary>
    private void SpawnEnemyAtIndex(int index)
    {
        GameObject enemy = Instantiate(_enemyPrefabs[index], transform.position, Quaternion.identity);

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
        _spawnDelay = GameManagerBehaviour.Instance.EnemySpawnDelay;
        _spawnCount = GameManagerBehaviour.Instance.EnemySpawnCount;
        _delayBetweenWaveGroups = GameManagerBehaviour.Instance.DelayBetweenWaveGroups;
        _wavesInGroup = GameManagerBehaviour.Instance.WavesInGroup;
    }

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

            _enemySpawnDelay = (_spawnDelay / _spawnCount) / 4;
            SpawnRandomIndexWave();
        }
    }
}
